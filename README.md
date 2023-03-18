# Juicy-Game-Feedback-by-AD
This powerful tool gives developers the power to quickly add an array of game feels and feedbacks to their games in just a 2 simple lines of code.

No more fussing with complex scripting, this tool has a custom inspector that makes it easy to add and remove feedbacks from your game with ease.

Features:

1) Shaking Camera: Bring intense moments to life by shaking the camera using either the Unity default camera or the Cinemachine virtual camera.
2) VFX Explosions: Ignite the senses with stunning visual effects that explode onto the scene.
3) Music and Sound Effects: Immerse players in the world with pulse-pounding music and sound effects that elevate the action.
4) Stunning Visuals: Transform your game's look with new post-processing profiles that make your world come alive.
5) Dynamic Events: Trigger dramatic events that keep players on the edge of their seats and drive the action forward.
6) Animated Action: Make your game come to life with smooth animations that alter the position, rotation, and scale of objects.

With these features, you can create games that are visually stunning, emotionally engaging, and unforgettable!


Uploading Juicy Game Feedbacks by AD.mp4…



Getting Started:


Install the "JuicyGameFeedbacks_byAD" asset from the Unity Asset Store.
Creating Player Jump Feedback:

Open your script where you want to add the FEEL.
Add a serialized Feedback_Base variable to represent the player jump feedback:


```
[SerializeField] private Feedback_Base PlayerJumpFeedback;
```

In your player controller's jump function, call the Play() method on the PlayerJumpFeedback variable:


```
private void Jump()
{
PlayerJumpFeedback.Play();
// Jump Logic
}
```

Customizing Feedback:

You can now customize the feedbacks in the inspector by selecting the Feedback_Base variable in the inspector and adjusting the parameters to your desired values.


That's it! You have successfully added a player jump feedback to your game with just two lines of code. Enjoy using the JuicyGameFeedbacks_byAD asset to bring life to your game


NOTE : Built-in RP does not support post-processing effects. For the best experience, we recommend using URP or HDRP.
