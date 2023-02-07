using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Events;

[System.Serializable]
public class FeedBack_Base : MonoBehaviour
{
    [System.Serializable]
    public class CameraShakeSettings
    {
        [Space]
        public bool isUsingCinemachineCamera;
        [Space]
        public GameObject cameraGameObject;
        public float cameraShakeDuration;
        public float cameraShakeIntensity;
        [Header("Cinemachine")]
        [Space]
        public CinemachineVirtualCamera cinemachineCameraGameObject;
        public float cinemachineCameraShakeDuration;
        public float cinemachineCameraShakeIntensity;

    }
    [System.Serializable]
    public class VFXSettings
    {
        [Space]
        public GameObject vfxObject;
        public Transform vfxSpawnPoint;
        public Vector3 vfxSpawnOffset;
        public float vfxExpirationTime;
    }

    [System.Serializable]
    public class SFXSettings
    {
        [Space]
        public AudioClip sfxClip;
        [Range(0, 1)]
        public float sfxVolume;
        public float sfxExpirationTime;
    }
    [System.Serializable]
    public class PostProcessingSettings
    {
        [Space]
        public Volume globalVolume;
        [Space]
        public VolumeProfile defaultProfile;
        public VolumeProfile activeProfile;
        public float profileExpirationTime;

    }
    [System.Serializable]
    public class TriggerEvent
    {
        public UnityEvent OnFeedbackTrigger;
    }
    public bool UseCameraShake;
    [SerializeField]
    private CameraShakeSettings _cameraShakeSettings;
    public bool UseVFX;
    [SerializeField]
    private VFXSettings _vFXSettings;
    public bool UseSFX;
    [SerializeField]
    private SFXSettings _sFXSettings;
    public bool UsePostProcessing;
    [SerializeField]
    private PostProcessingSettings _postProcessingSettings;
    public bool UseEvents;
    [SerializeField]
    private TriggerEvent _unityEvents;


    public CameraShakeSettings cameraShakeSettings
    {
        get { return _cameraShakeSettings; }
        set { _cameraShakeSettings = value; }
    }
    public VFXSettings vFXSettings
    {
        get { return _vFXSettings; }
        set { _vFXSettings = value; }
    }
    public SFXSettings sFXSettings
    {
        get { return _sFXSettings; }
        set { _sFXSettings = value; }
    }
    public PostProcessingSettings postProcessingSettings
    {
        get { return _postProcessingSettings; }
        set { _postProcessingSettings = value; }
    }
    public TriggerEvent triggerEvents
    {
        get { return _unityEvents; }
        set { _unityEvents = value; }
    }
    private void Start()
    {
        if (_postProcessingSettings.defaultProfile == null)
        {
            Debug.LogWarning("Postprocessing Default is null");
        }
        else
        {
            if(_postProcessingSettings.globalVolume == null)
            {
                Debug.LogWarning("PostProcessing Volume is null");
            }
            else
            {
                _postProcessingSettings.globalVolume.profile = _postProcessingSettings.defaultProfile;
            }
        }
    }
    void OnValidate()
    {
        if (_cameraShakeSettings == null)
        {
            Debug.LogWarning("Camera Shake Settings is null.");
        }
        else
        {
            if (_cameraShakeSettings.isUsingCinemachineCamera)
            {
                if (_cameraShakeSettings.cinemachineCameraGameObject == null)
                {
                    Debug.LogWarning("Cinemachine Camera Shake Game Object is null.");
                }
                else
                {
                    _cameraShakeSettings.cinemachineCameraGameObject.gameObject.SetActive(true);
                }
            }
            else
            {
                if (_cameraShakeSettings.cinemachineCameraGameObject == null)
                {
                    Debug.LogWarning("Cinemachine Camera Shake Game Object is null.");
                }
                else
                {
                    _cameraShakeSettings.cinemachineCameraGameObject.gameObject.SetActive(false);
                }
            }
        }
    }
    
    public void Play()
    {
        if (UseCameraShake)
        {
            StartCoroutine(ShakeCamera(_cameraShakeSettings.cameraGameObject, _cameraShakeSettings.cameraShakeDuration, _cameraShakeSettings.cameraShakeIntensity));
        }
        if (UseVFX)
        {
            if(_vFXSettings.vfxSpawnPoint == null)
            {
                Debug.LogWarning("VFX Spawn Position is null");
            }
            else
            {
                if (_vFXSettings.vfxObject == null)
                {
                    Debug.LogWarning("VFX Gameobject is null");
                }
                else
                {
                        GameObject vfxInstance = Instantiate(
                        _vFXSettings.vfxObject,
                        _vFXSettings.vfxSpawnPoint.position + _vFXSettings.vfxSpawnOffset,
                        Quaternion.identity
                    );
                    StartCoroutine(DeleteVFXAfterTime(vfxInstance, _vFXSettings.vfxExpirationTime));
                }
            }
        }
        if (UseSFX)
        {
            if (_sFXSettings.sfxClip == null)
            {
                Debug.LogWarning("SFX Clip is null");
            }
            else
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = _sFXSettings.sfxClip;
                audioSource.volume = _sFXSettings.sfxVolume;
                audioSource.Play();
                StartCoroutine(DeleteSFXAfterTime(audioSource));
            }
        }
        if(UsePostProcessing)
        {
            if(_postProcessingSettings.defaultProfile == null)
            {
                if (_postProcessingSettings.activeProfile == null)
                {
                    Debug.LogWarning("Postprocessing EffectToShow is null");
                }
                Debug.LogWarning("Postprocessing Default is null");
            }
            else
            {
                if(_postProcessingSettings.globalVolume == null) 
                {
                    Debug.LogWarning("Post Processing's Global Volume is null");
                }
                else
                {
                    _postProcessingSettings.globalVolume.profile = _postProcessingSettings.activeProfile;
                    StartCoroutine(RemoveEffect());
                }
            }
        }
        if (UseEvents)
        {
            _unityEvents.OnFeedbackTrigger?.Invoke();
        }
    }
    private IEnumerator DeleteVFXAfterTime(GameObject vfx, float time)
    {
        yield return new WaitForSeconds(time);
        if(vfx != null )
        {
            Destroy(vfx);
        }
        else
        {
            Debug.LogWarning("Cant Delete coz Spawned vfx is null");
        }
    }

    private IEnumerator DeleteSFXAfterTime(AudioSource audioSource)
    {
        yield return new WaitForSeconds(_sFXSettings.sfxExpirationTime);
        if (audioSource != null)
        {
            Destroy(audioSource);
        }
        else
        {
            Debug.LogWarning("Cant Delete coz spawned audiosource is null");
        }
    }
    IEnumerator RemoveEffect()
    {
        yield return new WaitForSeconds(_postProcessingSettings.profileExpirationTime);
        _postProcessingSettings.globalVolume.profile = _postProcessingSettings.defaultProfile;
    }
    private IEnumerator ShakeCamera(GameObject go, float time, float intensity)
    {
        if (_cameraShakeSettings != null)
        {
            if (_cameraShakeSettings.isUsingCinemachineCamera)
            {
                if (_cameraShakeSettings.cinemachineCameraGameObject != null)
                {
                    CinemachineBasicMultiChannelPerlin noise = _cameraShakeSettings.cinemachineCameraGameObject.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    if (noise != null)
                    {
                        noise.m_AmplitudeGain = _cameraShakeSettings.cinemachineCameraShakeIntensity;
                        noise.m_FrequencyGain = 1f;

                        yield return new WaitForSeconds(_cameraShakeSettings.cinemachineCameraShakeDuration);

                        noise.m_AmplitudeGain = 0f;
                        noise.m_FrequencyGain = 0f;
                    }
                    else
                    {
                        Debug.LogWarning("Cinemachine Basic Multi Channel Perlin component is not attached to the Cinemachine Camera Shake Game Object.");
                    }
                }
                else
                {
                    Debug.LogWarning("Cinemachine Camera Shake Game Object is null.");
                }
            }
            else
            {
                if (_cameraShakeSettings.cameraGameObject != null)
                {
                    Vector3 originalPos = _cameraShakeSettings.cameraGameObject.transform.localPosition;
                    float elapsedTime = 0.0f;

                    while (elapsedTime < _cameraShakeSettings.cameraShakeDuration)
                    {
                        float x = UnityEngine.Random.Range(-1f, 1f) * _cameraShakeSettings.cameraShakeIntensity;
                        float y = UnityEngine.Random.Range(-1f, 1f) * _cameraShakeSettings.cameraShakeIntensity;

                        _cameraShakeSettings.cameraGameObject.transform.localPosition = new Vector3(x, y, originalPos.z);

                        elapsedTime += Time.deltaTime;

                        yield return null;
                    }

                    _cameraShakeSettings.cameraGameObject.transform.localPosition = originalPos;
                }
                else
                {
                    Debug.LogWarning("Camera Shake Game Object is null.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Camera Shake Settings is null.");
        }


    }

}

