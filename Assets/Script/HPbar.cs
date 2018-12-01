using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProgressBar;

public class HPbar : MonoBehaviour {

    [SerializeField]
    private ProgressBarBehaviour HP = null;
    [SerializeField]
    private ProgressBarBehaviour MP = null;
    private ProgressBarBehaviour makedHPbar = null;
    private ProgressBarBehaviour makedMPbar = null;

    private bool displayHPMP = true;
    // Use this for initialization
    void Start () {
        makedHPbar = makeHPbar();
        makedMPbar = makeMPbar();
    }

    // Update is called once per frame
    void Update()
    {
        if (displayHPMP == true)
        {
            makedHPbar.gameObject.SetActive(true);
            makedMPbar.gameObject.SetActive(true);

            GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
            Vector3 pos = WorldToCanvas.toCanvas(canvas, transform.position + new Vector3(0, 3.2f));
            makedHPbar.transform.position = pos;

            Vector3 pos2 = WorldToCanvas.toCanvas(canvas, transform.position + new Vector3(0, 3.0f));
            makedMPbar.transform.position = pos2;
        }

        if(displayHPMP == false)
        {
            makedHPbar.gameObject.SetActive(false);
            makedMPbar.gameObject.SetActive(false);
        }
    }

    private ProgressBarBehaviour makeHPbar()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        Vector2 pos = WorldToCanvas.toCanvas(canvas, transform.position + new Vector3(0, 3.2f));
        ProgressBarBehaviour pb = Instantiate(HP, pos, Quaternion.identity);
        pb.ProgressSpeed = 500;
        pb.SetFillerSizeAsPercentage(100.0f);
        pb.transform.SetParent(canvas.transform, false);
        pb.transform.localScale = new Vector3(0.2f, 0.3f);

        return pb;
    }

    private ProgressBarBehaviour makeMPbar()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        Vector2 pos = WorldToCanvas.toCanvas(canvas, transform.position + new Vector3(0, 3.0f));
        ProgressBarBehaviour pb = Instantiate(MP, pos, Quaternion.identity);
        pb.ProgressSpeed = 500;
        pb.SetFillerSizeAsPercentage(100.0f);
        pb.transform.SetParent(canvas.transform, false);
        pb.transform.localScale = new Vector3(0.2f, 0.3f);

        return pb;
    }

    public ProgressBarBehaviour getHPbar()
    {
        return makedHPbar;
    }

    public ProgressBarBehaviour getMPbar()
    {
        return makedMPbar;
    }

    public void offbar()
    {
        displayHPMP = false;
    }
}
