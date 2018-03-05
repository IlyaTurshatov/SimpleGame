using UnityEngine;
public class LifeCycle : MonoBehaviour
{
    public float lifeTime = 20;
    public float matureAfter = 10;
    public float reproductionPause = 2;

    public Material young;
    public Material ready;
    public Material notReady;

    public GameObject individual;

    float deathTime;
    float matureTime;
    float nextReproduction;

    bool isMature = false;

    MeshRenderer renderer;

    public bool readyToReproduce = false;
    public bool canOnlyMove;

    private void Start()
    {
        transform.localScale = Vector3.one * 0.1f;
        renderer = GetComponent<MeshRenderer>();
        renderer.material = young;
        deathTime = Time.time + lifeTime;
        matureTime = Time.time + matureAfter;
    }

    private void Update()
    {

        if (Time.time > deathTime)
            Destroy(gameObject);

        if (!isMature)
        {
            if (transform.localScale.x <= 1)
                transform.localScale *= (1 + Time.deltaTime);
            else
                transform.localScale = Vector3.one;

            if (Time.time > matureTime)
            {
                renderer.material = ready;
                isMature = true;
                readyToReproduce = true;
            }
        }
        else
        {
            if (canOnlyMove)
                return;

            if (readyToReproduce)
            {
                if (Time.time > nextReproduction + 2)
                {
                    if (DetectPartner())
                    {
                        GiveBirth();
                        readyToReproduce = false;
                        renderer.material = notReady;
                    }
                }
            }
            else
            {
                if (Time.time > nextReproduction)
                {
                    readyToReproduce = true;
                    renderer.material = ready;
                }
            }
        }
    }

    void GiveBirth()
    {
        nextReproduction = Time.time + reproductionPause;
        var g = Instantiate(individual, transform.position, Quaternion.identity);
        g.GetComponent<LifeCycle>().canOnlyMove = true;
    }

    bool DetectPartner()
    {
        var partners = Physics.OverlapSphere(transform.position, 0.5f);
        foreach(var p in partners)
        {
            if (p.gameObject == this.gameObject)
                continue;
            var pLifeCycle = p.GetComponent<LifeCycle>();
            if (pLifeCycle.readyToReproduce)
            {
                return true;
            }
        }
        return false;
    }
}

