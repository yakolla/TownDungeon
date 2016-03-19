using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Arrow : MonoBehaviour {
    
    Vector3 m_start;
    [SerializeField]
    Vector3 m_end = Vector3.zero;
    [SerializeField]
    float m_height = 3f;
    [SerializeField]
    float m_speed = 0f;

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

    public void Init(Vector3 start, Vector3 end, float height, float speed, System.Action finishCallback)
    {
        transform.position = start;
        m_end = end;
        m_height = height;
        m_speed = speed;
        m_finishCallback = finishCallback;
    }

    void Update()
    {
        if (m_finish == true)
            return;

        m_prevPos = transform.position;
        transform.position = Parabola.Update(m_start, m_end, m_height, m_elapseTime);
        if (transform.position.y < m_end.y)
        {
            transform.position = new Vector3(transform.position.x, m_end.y, transform.position.z);
            m_finish = true;

            if (m_finishCallback != null)
                m_finishCallback();

            StartCoroutine(LoopDeath());
        }
        transform.LookAt(m_prevPos);
        m_elapseTime += Time.deltaTime* m_speed;
    }
}
