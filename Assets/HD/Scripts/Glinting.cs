using System.Collections;
using UnityEngine;

public class Glinting : MonoBehaviour
{
  /// <summary>
  /// 閃爍顏色
  /// </summary>
  public Color color = new Color(1, 0, 1, 1);
  /// <summary>
  /// 最低發光亮度，取值範圍[0,1]，需小於最高發光亮度。
  /// </summary>
  [Range(0.0f, 1.0f)]
  public float minBrightness = 0.0f;
  /// <summary>
  /// 最高發光亮度，取值範圍[0,1]，需大於最低發光亮度。
  /// </summary>
  [Range(0.0f, 1)]
  public float maxBrightness = 0.5f;
  /// <summary>
  /// 閃爍頻率，取值範圍[0.2,30.0]。
  /// </summary>
  [Range(0.2f, 30.0f)]
  public float rate = 1;

  [Tooltip("勾選此項則啓動時自動開始閃爍")]
  [SerializeField]
  private bool _autoStart = false;

  private float _h, _s, _v;      // 色調，飽和度，亮度
  private float _deltaBrightness;   // 最低最高亮度差
  private Renderer _renderer;
  private Material _material;
  private readonly string _keyword = "_EMISSION";
  private readonly string _colorName = "_EmissionColor";

  private Coroutine _glinting;

  private void Start()
  {
    _renderer = gameObject.GetComponent<Renderer>();
    _material = _renderer.material;

    if (_autoStart)
    {
      StartGlinting();
    }
  }

  /// <summary>
  /// 校驗數據，並保證運行時的修改能夠得到應用。
  /// 該方法只在編輯器模式中生效！！！
  /// </summary>
  private void OnValidate()
  {
    // 限制亮度範圍
    if (minBrightness < 0 || minBrightness > 1)
    {
      minBrightness = 0.0f;
      Debug.LogError("最低亮度超出取值範圍[0, 1]，已重置爲0。");
    }
    if (maxBrightness < 0 || maxBrightness > 1)
    {
      maxBrightness = 1.0f;
      Debug.LogError("最高亮度超出取值範圍[0, 1]，已重置爲1。");
    }
    if (minBrightness >= maxBrightness)
    {
      minBrightness = 0.0f;
      maxBrightness = 1.0f;
      Debug.LogError("最低亮度[MinBrightness]必須低於最高亮度[MaxBrightness]，已分別重置爲0/1！");
    }

    // 限制閃爍頻率
    if (rate < 0.2f || rate > 30.0f)
    {
      rate = 1;
      Debug.LogError("閃爍頻率超出取值範圍[0.2, 30.0]，已重置爲1.0。");
    }

    // 更新亮度差
    _deltaBrightness = maxBrightness - minBrightness;

    // 更新顏色
    // 注意不能使用 _v ，否則在運行時修改參數會導致亮度突變
    float tempV = 0;
    Color.RGBToHSV(color, out _h, out _s, out tempV);
  }

  /// <summary>
  /// 開始閃爍。
  /// </summary>
  public void StartGlinting()
  {
    _material.EnableKeyword(_keyword);

    if (_glinting != null)
    {
      StopCoroutine(_glinting);
    }
    _glinting = StartCoroutine(IEGlinting());
  }

  /// <summary>
  /// 停止閃爍。
  /// </summary>
  public void StopGlinting()
  {
    _material.DisableKeyword(_keyword);

    if (_glinting != null)
    {
      StopCoroutine(_glinting);
    }
  }

  /// <summary>
  /// 控制自發光強度。
  /// </summary>
  /// <returns></returns>
  private IEnumerator IEGlinting()
  {
    Color.RGBToHSV(color, out _h, out _s, out _v);
    _v = minBrightness;
    _deltaBrightness = maxBrightness - minBrightness;

    bool increase = true;
    while (true)
    {
      if (increase)
      {
        _v += _deltaBrightness * Time.deltaTime * rate;
        increase = _v <= maxBrightness;
      }
      else
      {
        _v -= _deltaBrightness * Time.deltaTime * rate;
        increase = _v <= minBrightness;
      }
      _material.SetColor(_colorName, Color.HSVToRGB(_h, _s, _v));
      //_renderer.UpdateGIMaterials();
      yield return null;
    }
  }
}