using Module03.Base;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Module03.Turret
{
    public class DragAndDropTurret : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject _turretPrefab;
        [SerializeField] private int _turretCostInEnergyPoints;
        [SerializeField] private TextMeshProUGUI _turretInfosText;
        
        private Image _image;
        private GameObject _clone;
        private BaseController _baseController;
        private bool _canPurchaseTurret;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _baseController = FindObjectOfType<BaseController>();
            _baseController.OnEnergyPointsChanged += EnableTurretPurchase;
            
            // initialize the turret stats
            TurretController turretController = _turretPrefab.GetComponent<TurretController>();
            _turretInfosText.text = _turretCostInEnergyPoints + "\n" + (1 + turretController.BasicsDamages) + "\n" + turretController.FireRate;
        }

        public void OnBeginDrag(PointerEventData eventData)
        { 
            if (!_canPurchaseTurret)
                return;
            
            // do a clone so the turret icon stay in the bar
            _clone = Instantiate(gameObject, transform.position, transform.rotation);
            _clone.transform.SetParent(transform.root);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_canPurchaseTurret)
                return;
            
            _clone.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_canPurchaseTurret)
                return;
            
            Destroy(_clone);
            
            // launch a ray from the mouse position to detect if the mouse is over a turret base
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
            
            if (hit.collider != null && hit.collider.CompareTag("TurretBase"))
            {
                // if the turret base already has a turret, don't place a new one
                if (hit.transform.childCount != 0)
                    return;
                
                GameObject turret = Instantiate(_turretPrefab);
                turret.transform.SetParent(hit.transform);
                // center the turret on the base
                turret.transform.position = hit.transform.position;
                
                _baseController.RemoveEnergy(_turretCostInEnergyPoints);
            }
        }
        
        private void EnableTurretPurchase(int energyPoints)
        {
            if (energyPoints < _turretCostInEnergyPoints)
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 0.3f);
                _canPurchaseTurret = false;
            }
            else
            {
                _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1.0f);
                _canPurchaseTurret = true;
            }
        }

        private void OnDestroy()
        {
            _baseController.OnEnergyPointsChanged -= EnableTurretPurchase;
        }
    }
}