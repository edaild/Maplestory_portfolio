using UnityEngine;

public class FieldObject : MonoBehaviour
{
    public PlayerMoveMent playerMoveMent;

    private void Start()
    {
        playerMoveMent = GetComponent<PlayerMoveMent>();
    }

}
