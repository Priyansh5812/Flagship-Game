using UnityEngine;


public class EnemyMovement : MonoBehaviour 
{
    [SerializeField] private float originalY;
    [SerializeField] private float p2BOffset;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float startingRad;
    [Range(0f , 10)]
    [SerializeField] private float sinOffset; //Y Axis offset
    [Range(1f, 30f)]
    [SerializeField] private float sinSpeed;
    
    // CONSTANTS
    private const float TWO_PI = 6.28f;


    private void Start()
    {    
        originalY = this.transform.position.y;
        startingRad = Random.Range(0f, TWO_PI);
    }


    public void Move()
    {
        if (IsCloseEnough())
            return;
        
        this.transform.position = this.transform.position + this.transform.forward * moveSpeed * Time.deltaTime;

    }
    // Movement for Bats and Bees
    public void Apply_SinVariations()
    {
        startingRad += sinSpeed * Time.deltaTime;
        if(startingRad > TWO_PI)
            startingRad = TWO_PI;
        startingRad %= TWO_PI;
        this.transform.position = new Vector3(this.transform.position.x, originalY+(Mathf.Sin(startingRad) * sinOffset), this.transform.position.z);

    }



    private bool IsCloseEnough()
    {
        Vector3 p2B = this.transform.position - Player.main.position; // Player ----------------> Bat
        if (Vector3.Dot(Player.main.forward, p2B) <= p2BOffset)
            return true;
        else
            return false;   
    }

    //TODO
    // Need a function which sets the default value for sinOffset, moveSpeed, sinSpeed... When enemy is close enough
    


}
