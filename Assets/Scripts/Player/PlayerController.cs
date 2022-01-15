using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float horizMoveSpeed;
    public float vertMoveSpeed;

    private void Update() {
        DoMove();
    }

    private void DoMove() {
        var hInput = Input.GetAxisRaw("Horizontal");
        var vInput = Input.GetAxisRaw("Vertical");

        var horizComponent = 0f;
        var vertComponent = 0f;

        if (hInput != 0) {
            horizComponent = horizMoveSpeed * hInput;
        }

        if (vInput != 0) {
            vertComponent = vertMoveSpeed * vInput;
        }

        var translation = new Vector2(horizComponent, vertComponent);

        if (!Mathf.Approximately(horizComponent, 0f) && !Mathf.Approximately(vertComponent, 0f)) {
            var a = horizMoveSpeed;
            var b = vertMoveSpeed;
            var x = horizComponent;
            var y = vertComponent;

            var theta = Mathf.Atan(y / x);
            var sinTheta = Mathf.Sin(theta);
            var cosTheta = Mathf.Cos(theta);

            var sinComponent = Mathf.Pow(x, 2) * Mathf.Pow(sinTheta, 2);
            var cosComponent = Mathf.Pow(y, 2) * Mathf.Pow(cosTheta, 2);

            var r = a * b / Mathf.Sqrt(sinComponent + cosComponent);

            // TODO seems scuffed, there's probly a math reason why the directions are reversed while moving left. look into this
            var reverseFlag = horizComponent < 0f ? -1f : 1f;

            translation = new Vector2(Mathf.Cos(theta) * r * reverseFlag, Mathf.Sin(theta) * r * reverseFlag);
        }

        transform.Translate(translation * Time.deltaTime);
    }
}
