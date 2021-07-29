using UnityEngine;

public class SnowBrush : MonoBehaviour
{
    public CustomRenderTexture SnowHeightMap;
    public Material HeightMapUpdate;

    public float SecondsToRestore = 100;

    public GameObject Player;
    //public GameObject[] Paws;

    private Camera _mainCamera;
    private int _tireIndex;

    private float _timeToRestoreOneTick;
    
    private static readonly int DrawPosition = Shader.PropertyToID("_DrawPosition");
    private static readonly int DrawAngle = Shader.PropertyToID("_DrawAngle");
    private static readonly int RestoreAmount = Shader.PropertyToID("_RestoreAmount");

    private void Start()
    {
        SnowHeightMap.Initialize();
        Player = GameObject.Find("Player");
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        DrawWithTires();

        // Считаем таймер до восстановления каждого пикселя текстуры на единичку 
        _timeToRestoreOneTick -= Time.deltaTime;
        if (_timeToRestoreOneTick < 0)
        {
            // Если в этот update мы хотим увеличить цвет всех пикселей карты высот на 1
            HeightMapUpdate.SetFloat(RestoreAmount, 1 / 250f);
            _timeToRestoreOneTick = SecondsToRestore / 250f;
        }
        else
        {
            // Если не хотим
            HeightMapUpdate.SetFloat(RestoreAmount, 0);
        }
        
        // Обновляем текстуру вручную, можно это убрать и поставить Update Mode: Realtime
        SnowHeightMap.Update();
    }

    private void DrawWithTires()
    {
        Ray ray = new Ray(Player.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector2 hitTextureCoord = hit.textureCoord;
            float angle = Player.transform.rotation.eulerAngles.y;

            HeightMapUpdate.SetVector(DrawPosition, hitTextureCoord);
            HeightMapUpdate.SetFloat(DrawAngle, angle * Mathf.Deg2Rad);
        }
    }

    //private void DrawWithPaws()
    //{
    //    GameObject paw = Paws[tireIndex++ % Paws.Length];

    //    Ray ray = new Ray(paw.transform.position, Vector3.down);
    //    if (Physics.Raycast(ray, out RaycastHit hit, 0.32f))
    //    {
    //        Vector2 hitTextureCoord = hit.textureCoord;
    //        float angle = 180 + paw.transform.rotation.eulerAngles.y;

    //        HeightMapUpdate.SetVector(DrawPosition, hitTextureCoord);
    //        HeightMapUpdate.SetFloat(DrawAngle, angle * Mathf.Deg2Rad);
    //    }
    //}

    //private void DrawWithMouse(Vector2 worldMousePosition)
    //{
    //    Vector2 mousePosition = mainCamera.WorldToScreenPoint(worldMousePosition);
    //    Ray ray = mainCamera.ScreenPointToRay(mousePosition);

    //    if (Physics.Raycast(ray, out RaycastHit hit))
    //    {
    //        Vector2 hitTextureCoord = hit.textureCoord;

    //        HeightMapUpdate.SetVector(DrawPosition, hitTextureCoord);
    //        HeightMapUpdate.SetFloat(DrawAngle, 45 * Mathf.Deg2Rad);
    //    }
    //}
}
