using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EntryDoor : MonoBehaviour {

    private Animator anim = null;

    private void Start() {
        anim = GetComponent<Animator>();
        transform.parent = null;
        Invoke("PlayAnimation", 0.5f);
    }

    private void PlayAnimation() {
        anim.Play("Close");
        Invoke("Disappear", 0.5f);
    }

    private void Disappear() {
        Destroy(gameObject);
    }
}
