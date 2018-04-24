using UnityEngine;

public class MovePaddle : MonoBehaviour {
    [SerializeField] float speed = 30;
    [SerializeField] string axis = "Vertical";

    public void Start()
    {
        SingleOrMultiPlayer();
    }

    private void SingleOrMultiPlayer()
    {
        if (axis == "Vertical2")
        {
            if (MainMenu.isSinglePlayer)
            {
                gameObject.GetComponent<MovePaddle>().enabled = false;
                gameObject.GetComponent<AIPaddle>().enabled = true;
            }

            else
            {
                gameObject.GetComponent<MovePaddle>().enabled = true;
                gameObject.GetComponent<AIPaddle>().enabled = false;
            }
        }
    }

    void FixedUpdate()
    {
        float vertPress = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, vertPress) * speed;
    }
}
