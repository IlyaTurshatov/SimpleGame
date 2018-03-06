using UnityEngine;
public class LifeCycle : MonoBehaviour
{
    public enum State
    {
        young,
        mature,
        pregnant
    };

    public State state = State.young; 

    public float lifeTime = 20;
    public float matureAfter = 10;

    public bool isFemale = true;

    public float pregnance = 3;
    public int childNumber = 3;

    public Material youngMaterial;
    public Material readyMaterial;
    public Material pregnantMaterial;

    public GameObject individual;

    float deathTime;
    float matureTime;
    float giveBirthTime;

    bool isGrowingUp = true;

    MeshRenderer renderer;
    Statistics statistics;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        statistics = GameObject.Find("Statistics").GetComponent<Statistics>();
        isGrowingUp = true;
        SetYoung();
        statistics.NewSheep(this);
    }

    private void Update()
    {

        if (Time.time > deathTime)
        {
            statistics.Died(this);
            Destroy(gameObject);
        }

        switch (state)
        {
            case State.young:
                GrowUp();
                if (Time.time > matureTime)
                {
                    statistics.Mature();
                    SetMature();
                }
                break;

            case State.mature :
                if (!isFemale)
                    break;
                if (!DetectPartner())
                    break;
                statistics.Pregnant();
                SetPregnant();
                giveBirthTime = Time.time + pregnance;
                break;

            case State.pregnant :
                if (Time.time < giveBirthTime)
                    break;
                statistics.NotPregnant();
                GiveBirth();
                SetMature();
                break;
            default : break;
        }
    }

    private void GrowUp()
    {
        if (isGrowingUp)
        {
            if (transform.localScale.x <= 1)
                transform.localScale *= (1 + Time.deltaTime);
            else
            {
                isGrowingUp = false;
                transform.localScale = Vector3.one;
            }
        }
    }

    private void SetPregnant()
    {
        state = State.pregnant;
        renderer.material = pregnantMaterial;
    }

    private void SetMature()
    {
        state = State.mature;
        renderer.material = readyMaterial;
    }

    private void SetYoung()
    {
        state = State.young;
        renderer.material = youngMaterial;

        transform.localScale = Vector3.one * 0.1f;
        lifeTime += Random.Range(-2f, 2f);
        deathTime = Time.time + lifeTime;
        matureTime = Time.time + matureAfter;
    }

    void GiveBirth()
    {
        for (int i = 0; i < childNumber; i++)
        {
            var g = Instantiate(individual, transform.position, Quaternion.identity);
            g.name = "Sheep";
            g.GetComponent<LifeCycle>().isFemale = Random.Range(0.0f, 1.0f) >= 0.5f ? true : false;
            var r = g.GetComponent<LifeCycle>();
        }
    }

    bool DetectPartner()
    {
        var partners = Physics.OverlapSphere(transform.position, 0.6f);
        foreach(var p in partners)
        {
            if (p.gameObject == this.gameObject)
                continue;

            var pLifeCycle = p.GetComponent<LifeCycle>();

            if (pLifeCycle == null)
                continue;
            if (pLifeCycle.state == State.mature && !pLifeCycle.isFemale)
                return true;
        }
        return false;
    }
}

