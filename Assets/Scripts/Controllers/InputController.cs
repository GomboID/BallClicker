using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private void OnEnable()
    {
        GameController.Instance.Action_GameEnd += StopInput;
    }

    private void OnDisable()
    {
        GameController.Instance.Action_GameEnd -= StopInput;
    }

    private void StopInput()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50f))
            {
                IClickable obj = hit.transform.GetComponent<IClickable>();

                if (obj != null)
                {
                    obj.ClickObject();
                }
            }
        }
    }
}
