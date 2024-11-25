using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera _camera;
    private HighLightTileCommand _highLightTileCommand;
    private Ray _ray;
    
    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        var hitObject = SetUpRaycast(); 
        
        if (hitObject&& Input.GetMouseButtonDown(0))
        {
            _highLightTileCommand = new HighLightTileCommand(hitObject);
            _highLightTileCommand.Execute();
        }

    }

    private GameObject SetUpRaycast()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition); 
        RaycastHit hit; 
        Physics.Raycast(_ray, out hit);
        
        return hit.collider ? hit.collider.gameObject : null;
    }
}