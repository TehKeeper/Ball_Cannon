using System;
using logic.behaviour;
using Unity.VisualScripting;
using UnityEngine;

namespace main
{
    public class InputController
    {
        private readonly IShootBehaviour _behaviour;
        private readonly Action<float> _onShoot;
        private readonly Action<float> _onPowUpd;

        private static InputController _instance;
        //public static InputController Instance => _instance ??= new InputController();

        public InputController(IShootBehaviour behaviour, Action<float> onShoot, Action<float> onPowUpd)
        {
            _behaviour = behaviour;
            _onShoot = onShoot;
            _onPowUpd = onPowUpd;
            _cam = Camera.main;
        }

        public Vector2 PointerPos() => Input.mousePosition;
        private Camera _cam;

        /*public InputController Instance()
        {
            if (_instance == null)
                _instance = new InputController();

            return _instance;
        }*/

        public void UpdateLooks(params Transform[] targTfs)
        {
            foreach (var ttf in targTfs)
            {
                PointToRay(ttf);
            }
        }

        private void PointToRay(Transform lookTf)
        {
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000))
            {
                lookTf.LookAt(hit.point);
            }
        }

        public void Update()
        {
            _behaviour.UpdatePower();
            _onPowUpd?.Invoke(_behaviour.GetPower());
            if (Input.GetKeyDown(KeyCode.Mouse0))
                OnMouseDownEvent();

            if (Input.GetKeyUp(KeyCode.Mouse0))
                OnMouseUpEvent();
        }

        private void OnMouseDownEvent()
        {
            _behaviour.SetTime();
        }

        private void OnMouseUpEvent()
        {
            _onShoot?.Invoke(_behaviour.GetPower());
            _behaviour.Reset();
        }
    }
}