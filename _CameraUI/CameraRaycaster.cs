using UnityEngine;
using UnityEngine.EventSystems;
using RPG.Characters; 

namespace RPG.CameraUI
{
    public class CameraRaycaster : MonoBehaviour
    {
        [SerializeField] Texture2D walkCursor = null;
        [SerializeField] Texture2D enemyCursor = null;
        [SerializeField] Vector2 cursorHotspot = new Vector2(0, 0);

        const int walkable_layer = 8;
        float maxRaycastDepth = 100f; 

        Rect screenRectAtStartPlay = new Rect(0, 0, Screen.width, Screen.height);

        public delegate void OnMouseOverEnemy(EnemyAI enemy);
        public event OnMouseOverEnemy onMouseOverEnemy;

        public delegate void OnMouseOverTerrain(Vector3 destination);
        public event OnMouseOverTerrain onMouseOverWalkable;

        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // Impliment UI interaction
            }
            else
            {
                PerformRaycasts();
            }
        }

        void PerformRaycasts()
        {
            if (screenRectAtStartPlay.Contains(Input.mousePosition))    //if mouse is on game screen
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                // Specify layer priorities below, order matters
                if (RaycastForEnemy(ray)) { return; }
                if (RaycastForWalkable(ray)) { return; }
            }
        }

        bool RaycastForEnemy(Ray ray)
        {
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, maxRaycastDepth);
            var gameObjectHit = hitInfo.collider.gameObject;
            var enemyHit = gameObjectHit.GetComponent<EnemyAI>();   //to know if object that was hit is enemy
            if (enemyHit)
            {
                Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);  //changing cursor
                onMouseOverEnemy(enemyHit); //delegate
                return true;    //we are pointing at the enemy
            }
            return false;
        }

        private bool RaycastForWalkable(Ray ray)
        {
            RaycastHit hitInfo;
            LayerMask potentiallyWalkableLayer = 1 << walkable_layer; //ignore all layers but walkable_layer
            bool potentiallyWalkableHit = Physics.Raycast(ray, out hitInfo, maxRaycastDepth, potentiallyWalkableLayer); //if it is walkable layer
            if (potentiallyWalkableHit) //if cursor is on walkable layer
            {
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto); //changing cursor model
                onMouseOverWalkable(hitInfo.point); //script PlayerControl uses this delegate to move/attack 
                return true;
            }
            return false;
        }
    }
}