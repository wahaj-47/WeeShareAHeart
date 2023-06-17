using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag != "Compund")
        {
            if (other.collider.tag == "Human")
            {
                PlayerStateManager manager = other.collider.gameObject.GetComponent<PlayerStateManager>();
                manager.SwitchState(manager.HumanAttackedState);
            }
            else
            {
                Destroy(other.gameObject);

                Vector2 point = other.GetContact(0).point;
                point.y = point.y - 0.1f;

                Sprite sprite = other.gameObject.GetComponent<SpriteRenderer>().sprite;

                GameObject victim = new GameObject();
                SpriteRenderer victimRenderer = victim.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                Fade fadeScript = victim.AddComponent(typeof(Fade)) as Fade;
            
                if (other.gameObject.TryGetComponent<Stabbable>(out Stabbable stabbable))
                {
                    sprite = stabbable.sprite;
                    victimRenderer.flipX = stabbable.flipX;
                    victimRenderer.flipY = stabbable.flipY;
                }

                victimRenderer.sprite = sprite;

                victim.transform.position = point;
                victim.transform.parent = this.transform;

                StartCoroutine(fadeScript.Die());
            }
        }
    }
}