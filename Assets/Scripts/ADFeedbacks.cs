using System;
using UnityEngine;
using System.Collections;
using Cinemachine;
using UnityEngine.Rendering;

[System.Serializable]
public class ADFeedbacks : MonoBehaviour
{
    
    [System.Serializable]
    public class CameraShakeSettings
    {
        [Space]
        public bool isCinemachineCamera;
        [Space]
        public GameObject CameraShakeGO;
        public float CameraShakeTime;
        public float CameraShakeIntensity;
        [Header("Cinemachine")]
        [Space]
        public CinemachineVirtualCamera CinemachineCameraShakeGO;
        public float CinemachineCameraShakeTime;
        public float CinemachineCameraShakeIntensity;

    }
    [System.Serializable]
    public class VFXSettings
    {
        [Space]
        public GameObject VFXGO;
        public Transform VFXSpawnPosition;
        public Vector3 VFXSpawnOffset;
        public float VFXDeletionTime;
    }

    [System.Serializable]
    public class SFXSettings
    {
        [Space]
        public AudioClip SFXClip;
        [Range(0, 1)]
        public float SFXVolume;
        public float SFXDeletionTime;
    }
    [System.Serializable]
    public class PostProcessingSettings
    {
        [Space]
        public Volume GlobalVolume;
        [Space]
        public VolumeProfile defaultEffect;
        public VolumeProfile EffectToShow;
        public float RemoveAfter;

    }
    public bool CameraShake;
    [SerializeField]
    private CameraShakeSettings _cameraShakeSettings;
    public bool VFX;
    [SerializeField]
    private VFXSettings _vFXSettings;
    public bool SFX;
    [SerializeField]
    private SFXSettings _sFXSettings;
    public bool PostProcessing;
    [SerializeField]
    private PostProcessingSettings _postProcessingSettings;

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
    private void Start()
    {
       
        if (_postProcessingSettings.defaultEffect == null)
        {
            Debug.LogWarning("Postprocessing Default is null");
        }
        else
        {
            if(_postProcessingSettings.GlobalVolume == null)
            {
                Debug.LogWarning("PostProcessing Volume is null");
            }
            else
            {
                _postProcessingSettings.GlobalVolume.profile = _postProcessingSettings.defaultEffect;
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
            if (_cameraShakeSettings.isCinemachineCamera)
            {
                if (_cameraShakeSettings.CinemachineCameraShakeGO == null)
                {
                    Debug.LogWarning("Cinemachine Camera Shake Game Object is null.");
                }
                else
                {
                    _cameraShakeSettings.CinemachineCameraShakeGO.gameObject.SetActive(true);
                }
            }
            else
            {
                if (_cameraShakeSettings.CinemachineCameraShakeGO == null)
                {
                    Debug.LogWarning("Cinemachine Camera Shake Game Object is null.");
                }
                else
                {
                    _cameraShakeSettings.CinemachineCameraShakeGO.gameObject.SetActive(false);
                }
            }
        }
    }
    
    public void Play()
    {
        if (CameraShake)
        {
            StartCoroutine(ShakeCamera(_cameraShakeSettings.CameraShakeGO, _cameraShakeSettings.CameraShakeTime, _cameraShakeSettings.CameraShakeIntensity));
        }
        if (VFX)
        {
            if(_vFXSettings.VFXGO == null)
            {
                Debug.LogWarning("VFX Gameobject is null");
                if(_vFXSettings.VFXSpawnPosition == null)
                {
                    Debug.LogWarning("VFX Spawn Position is null");
                }
            }
            else
            {
                GameObject vfxInstance = Instantiate(
                    _vFXSettings.VFXGO,
                    _vFXSettings.VFXSpawnPosition.position + _vFXSettings.VFXSpawnOffset,
                    Quaternion.identity
                );
                StartCoroutine(DeleteVFXAfterTime(vfxInstance, _vFXSettings.VFXDeletionTime));
            }
        }
        if (SFX)
        {
            if (_sFXSettings.SFXClip == null)
            {
                Debug.LogWarning("SFX Clip is null");
            }
            else
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = _sFXSettings.SFXClip;
                audioSource.volume = _sFXSettings.SFXVolume;
                audioSource.Play();
                StartCoroutine(DeleteSFXAfterTime(audioSource));
            }
        }
        if(PostProcessing)
        {
            if(_postProcessingSettings.defaultEffect == null)
            {
                if (_postProcessingSettings.EffectToShow == null)
                {
                    Debug.LogWarning("Postprocessing EffectToShow is null");
                }
                Debug.LogWarning("Postprocessing Default is null");
            }
            else
            {
                if(_postProcessingSettings.GlobalVolume == null) 
                {
                    Debug.LogWarning("Post Processing's Global Volume is null");
                }
                else
                {
                    _postProcessingSettings.GlobalVolume.profile = _postProcessingSettings.EffectToShow;
                    StartCoroutine(RemoveEffect());
                }
            }
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
        yield return new WaitForSeconds(_sFXSettings.SFXDeletionTime);
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
        yield return new WaitForSeconds(_postProcessingSettings.RemoveAfter);
        _postProcessingSettings.GlobalVolume.profile = _postProcessingSettings.defaultEffect;
    }
    private IEnumerator ShakeCamera(GameObject go, float time, float intensity)
    {
        if (_cameraShakeSettings != null)
        {
            if (_cameraShakeSettings.isCinemachineCamera)
            {
                if (_cameraShakeSettings.CinemachineCameraShakeGO != null)
                {
                    CinemachineBasicMultiChannelPerlin noise = _cameraShakeSettings.CinemachineCameraShakeGO.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                    if (noise != null)
                    {
                        noise.m_AmplitudeGain = _cameraShakeSettings.CinemachineCameraShakeIntensity;
                        noise.m_FrequencyGain = 1f;

                        yield return new WaitForSeconds(_cameraShakeSettings.CinemachineCameraShakeTime);

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
                if (_cameraShakeSettings.CameraShakeGO != null)
                {
                    Vector3 originalPos = _cameraShakeSettings.CameraShakeGO.transform.localPosition;
                    float elapsedTime = 0.0f;

                    while (elapsedTime < _cameraShakeSettings.CameraShakeTime)
                    {
                        float x = UnityEngine.Random.Range(-2f, 2f) * _cameraShakeSettings.CameraShakeIntensity;
                        float y = UnityEngine.Random.Range(-2f, 2f) * _cameraShakeSettings.CameraShakeIntensity;

                        _cameraShakeSettings.CameraShakeGO.transform.localPosition = new Vector3(x, y, originalPos.z);

                        elapsedTime += Time.deltaTime;

                        yield return null;
                    }

                    _cameraShakeSettings.CameraShakeGO.transform.localPosition = originalPos;
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

