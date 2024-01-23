using logic.behaviour;
using logic.world.ball;
using logic.world.gate;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using utilities;
using utilities.tools.mono;

namespace main
{
    public class GameController : MonoBehaviourUi
    {
        [SerializeField] private Transform cannonRoot;
        [SerializeField] private float cannonForce = 1;
        [SerializeField] private GateObject gate ;

        private TMP_Text _scoreText;
        private Image _gaugeFill;
        private InputController _input;
        private Image _gaugeImage;
        private SoccerBall _soccerBall;
        private Transform _launchPoint;
        private PrefabStorage _storage;
        private int _score;


        protected override void MakeInit()
        {
            Cursor.visible = false;
            _launchPoint = cannonRoot.FindTransform("Cannon_Launch_Point");
            _scoreText = tf.FindComponent<TMP_Text>("Score Text");
            _gaugeFill = tf.FindComponent<Image>("GaugeMask");
            _gaugeImage = _gaugeFill.transform.FindComponent<Image>("GaugeFill");

            _input = new InputController(new LinearPower(0.5f), ShootBall, UpdatePowerValue);

            _soccerBall = Resources.Load<SoccerBall>("SoccerBall");
            _storage = new PrefabStorage(new GameObject("Ball_Storage").transform, _soccerBall);
            
            gate.SetUp(OnBallCollision);
        }

        private void OnBallCollision(Transform obj)
        {
            if(!_storage.HasCollision(obj))
                return;

            ScorePoint();
            _storage.DespawnCollision(obj);
        }

        private void ScorePoint()
        {
            _scoreText.text = $"Счет: {++_score}";
        }

        private void UpdatePowerValue(float powerValue)
        {
            _gaugeFill.fillAmount = powerValue;
            var clr = Color.HSVToRGB(Mathf.Lerp(0.33f, 0.05f, powerValue), 1, Mathf.Lerp(0.8f, 1, powerValue));
            _gaugeImage.color = clr;
        }

        private void ShootBall(float power)
        {
            var ball = _storage.TryDequeueBall();
            ball.SetPos(_launchPoint.position);
            ball.Activate(true);
            ball.Shoot(power * cannonForce, _launchPoint.forward);
            ball.OnDespawn(_storage.EnqueueBall);
        }


        private void Update()
        {
            _input.UpdateLooks(cannonRoot);
            _input.Update();
        }
    }
}