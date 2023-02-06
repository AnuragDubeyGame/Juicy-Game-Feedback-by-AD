using System;
using UnityEngine;
using System.Collections;
using Cinemachine;

[System.Serializable]
public class ADFeedbacks : MonoBehaviour
{
    [System.Serializable]
    public class CameraShakeSettings
    {
        public GameObject CameraShakeGO;
        public float CameraShakeTime;
        public float CameraShakeIntensity;
    }

    [System.Serializable]
    public class VFXSettings
    {
        public GameObject VFXGO;
        public Transform VFXSpawnPosition;
        public Vector3 VFXSpawnOffset;
        public float VFXDeletionTime;
    }

    [System.Serializable]
    public class SFXSettings
    {
        public AudioClip SFXClip;
        [Range(0, 1)]
        public float SFXVolume;
        public float SFXDeletionTime;
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


    public void Play()
    {
        if (CameraShake)
        {
            StartCoroutine(ShakeCamera(_cameraShakeSettings.CameraShakeGO,_cameraShakeSettings.CameraShakeTime,_cameraShakeSettings.CameraShakeIntensity));
        }

        if (VFX)
        {
            // Code to play VFX
            GameObject vfxInstance = Instantiate(
                _vFXSettings.VFXGO,
                _vFXSettings.VFXSpawnPosition.position + _vFXSettings.VFXSpawnOffset,
                Quaternion.identity
            );
            StartCoroutine(DeleteVFXAfterTime(vfxInstance, _vFXSettings.VFXDeletionTime));
        }

        if (SFX)
        {
            // Code to play SFX
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = _sFXSettings.SFXClip;
            audioSource.volume = _sFXSettings.SFXVolume;
            audioSource.Play();
            StartCoroutine(DeleteSFXAfterTime(audioSource));
        }

    }
    private IEnumerator DeleteVFXAfterTime(GameObject vfx, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(vfx);
    }

    private IEnumerator DeleteSFXAfterTime(AudioSource audioSource)
    {
        yield return new WaitForSeconds(_sFXSettings.SFXDeletionTime);
        Destroy(audioSource);
    }
    private IEnumerator ShakeCamera(GameObject go, float time, float intensity)
    {
        Vector3 originalPos = go.transform.localPosition;
        float elapsedTime = 0.0f;

        while (elapsedTime < time)
        {
            float x = UnityEngine.Random.Range(-2f, 2f) * intensity;
            float y = UnityEngine.Random.Range(-2f, 2f) * intensity;

            go.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        go.transform.localPosition = originalPos;
    }
}

