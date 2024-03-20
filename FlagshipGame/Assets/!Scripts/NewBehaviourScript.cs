using UnityEngine;
using GenericServiceLocator;
using GenericServiceLocator.Templates;
public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        (ServiceLocator.Instance as IProvideService).PullService<InputManager>(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
