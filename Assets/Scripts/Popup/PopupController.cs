using UnityEngine;

public class PopupController : MonoBehaviour
{
    public static PopupController Instance;

    void Start()
    {
        if (Instance != null) {
            GameObject.Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    public Popup CreatePopup() {
        GameObject popUpGo = Instantiate(Resources.Load("PopUp") as GameObject);
        return popUpGo.GetComponent<Popup>();
    }
}