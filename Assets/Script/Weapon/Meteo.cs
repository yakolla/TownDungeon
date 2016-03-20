using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Meteo : MonoBehaviour {
    
    Vector3 m_start;
    [SerializeField]
    Vector3 m_end = Vector3.zero;
   
    [SerializeField]
    float m_speed = 0f;
    [SerializeField]
    GameObject m_prefHitFX;

    float m_elapseTime = 0;
    bool m_finish = false;

    Vector3 m_prevPos = Vector3.zero;

    System.Action m_finishCallback = null;
	
	void Start () {
        m_start = transform.position;
    }

    IEnumerator LoopDeath()
    {
        yield return new WaitForSeconds(2f);

        Vector3 pos = transform.position;
        pos.y = -3f;
        while (pos.y < transform.position.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime);
            yield return null;
        }

        GameObject.Destroy(gameObject);
    }

    public void Init(Vector3 start, Vector3 end, float speed, System.Action finishCallback)
    {
        transform.position = start;
        m_end = end;
        m_speed = speed;
        m_finishCallback = finishCallback;
    }

    void Update()
    {
        if (m_finish == true)
            return;

        m_prevPos = transform.position;
        transform.position = TrajectoryMeteo.Update(m_start, m_end, m_elapseTime);
        if (Vector3.Distance(transform.position, m_end) == 0f)
        {
            transform.position = new Vector3(transform.position.x, m_end.y, transform.position.z);
            m_finish = true;

            if (m_finishCallback != null)
                m_finishCallback();

            if (m_prefHitFX != null)
            {
                GameObject hitFXObj = Instantiate(m_prefHitFX) as GameObject;
                Vector3 pos = transform.position;
                pos.y = m_prefHitFX.transform.position.y;
                hitFXObj.transform.position = pos;
            }

            StartCoroutine(LoopDeath());
        }
        
        m_elapseTime += Time.deltaTime* m_speed;
    }
}
