using UnityEngine;

public class AudioManager
{
    private AudioSource musicAudio; //Musica
    private AudioSource SFXaudio; //Efecto de Sonido SFX
    private float volumenMusica = 1f;
    private float volumenSfx = 1f;
    private bool mutearMusica = false;


    public AudioManager(GameObject AudioObjeto)
    {

        musicAudio = AudioObjeto.AddComponent<AudioSource>();
        musicAudio.loop = true;

        SFXaudio = AudioObjeto.AddComponent<AudioSource>();
        SFXaudio.playOnAwake = false; // Cuando empiece la escena (cuando el objeto “despierte”), el AudioSource NO debe reproducir su audio automáticamente.
    }

    //MUSIC SOUND
    public void reproducirMusica(AudioClip musica)
    {
        if (musica == null) return;
        musicAudio.clip = musica;
        musicAudio.Play();
    }

    public void SilenciarMusica()
    {
        mutearMusica = !mutearMusica;
        musicAudio.mute = mutearMusica;
    }

    public void subirVolumenMusica(float cantidad = 0.1f)
    {
        volumenMusica = Mathf.Clamp(volumenMusica + cantidad, 0f, 1f);
        musicAudio.volume = volumenMusica;

    }

    public void bajarVolumenMusica(float cantidad = 0.1f)
    {
        volumenMusica = Mathf.Clamp(volumenMusica - cantidad, 0f, 1f);
        musicAudio.volume = volumenMusica;
    }

    //FBX SOUND

    public void reproducirFBX(AudioClip fbx)
    {
        if (fbx == null) return;
        SFXaudio.volume = volumenSfx;
        musicAudio.PlayOneShot(fbx, 1f);
    }

}
