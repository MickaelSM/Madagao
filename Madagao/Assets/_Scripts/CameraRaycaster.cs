using UnityEngine;

//Enum to store all the layers we wanna handle
public enum Layer { Walkable = 8, Enemy = 9, RaycastEndStop }

public class CameraRaycaster : MonoBehaviour
{
    //This is to define what the priorities are when raycasting for layers
    public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

    [SerializeField] float maxRaycastDepth = 100f;

    Camera m_cam;
    Layer currentLayer;

    //Create a delegate and an observer set to notify observers when a layer has changed
    public delegate void OnLayerChange(Layer newLayer);
    public event OnLayerChange onLayerChange;

    void Start()
    {
        m_cam = Camera.main;
    }

    void Update()
    {
        Raycast();
    }

    //Shoots a raycast looking for a specific layer
    RaycastHit? RaycastForLayer(Layer layer)
    {
        int layerMask = 1 << (int)layer; //Retrieves the layer mask for the layer we are looking for
        Ray ray = m_cam.ScreenPointToRay(Input.mousePosition); //Creates the raycast from our camera view to the mouse position

        RaycastHit hit; // used as an out parameter to store what we hit with the raycast
        bool hasHit = Physics.Raycast(ray, out hit, maxRaycastDepth, layerMask);//Shoots the raycast and stores the result
        //If we hit the layer we are looking for return the value of hit
        if (hasHit)
        {
            return hit;
        }
        return null; //Otherwise return null since we didnt find the layer we were looking for
    }

    // Looks for a layer and sets the current layer to the first layer hit according to the laterPriorities
    void Raycast()
    {
        foreach (Layer layer in layerPriorities)
        {
            var hit = RaycastForLayer(layer);
            //If we hit the current layer we are looking for then deal with it
            if (hit.HasValue)
            {
                if (currentLayer != layer) // if layer has changed
                {
                    currentLayer = layer; //Changes the current layerHit to the new layer hit
                    onLayerChange(layer); // call the delegates to notify that a layer has changed
                }
                return; //Prevents looking for more layers after already finding one
            }
        }
        // If no layer was hit set to background hit (should never happen) 
        currentLayer = Layer.RaycastEndStop;
    }
}
