using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [SerializeField] private Transform WaypointOne;
    [SerializeField] private Transform WaypointTwo;
    [SerializeField] private Rigidbody2D BatRigidBody;

    private float _direction = 0.01f;
    private Vector3 _leftPosition;
    private Vector3 _rightPosition;
    private Vector3 _verticalShift;

    private void Awake()
    {
        Vector3 positionOne = WaypointOne.transform.position;
        Vector3 positionTwo = WaypointTwo.transform.position;
        _verticalShift = new Vector3(0, (positionOne.y + positionTwo.y) / 2);
        
        if (positionOne.x < positionTwo.x)
        {
            _leftPosition = positionOne;
            _rightPosition = positionTwo;
        }
        else
        {
            _leftPosition = positionTwo;
            _rightPosition = positionOne;   
        }
    }
    
    void Update()
    {
        Vector3 currentPosition = transform.position;
        if (_direction > 0 && currentPosition.x > _rightPosition.x || 
            _direction < 0 && currentPosition.x < _leftPosition.x)
        {
            _direction *= -1;
        }
        
        float x = currentPosition.x + _direction;
        Vector3 target = new Vector3(x, Mathf.Sin(x * 20) / 10);
        BatRigidBody.MovePosition(target + _verticalShift);
    }
}
