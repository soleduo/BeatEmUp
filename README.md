# Brawler Game Development #

### Try it on itch.io ###

https://soleduo.itch.io/beatemup-prototype

### Main Focus ###

To achieve good fast-paced action-game feeling with minimum interaction. First development phase will focus on Player Control, Input feedback, and Interaction feedback. Editor tools to support this purposes shall be made.

### v0.1 ###

Core Mechanics up-and-running:

Two buttons for attack/move direction input.

Character Script: Health, Movement, and Attack.

Combo (continuous attack with shorter frame delay between attacks) can be made whenever an Attack connects, however when an Attack does not connects, it should take at least one second of recovery time before doing another command. 

Knockback State shall be applied when a Character receives an Attack. 

### v0.2 ###

Core Mechanics Tuning:

Timing window for combo input is -3 frame to +6 frame from Attack Blow frame (10 frame in total).

Replace UnityAnimationEvent with frameCount on Attack parameter to handle Attack States (Anticipation, Blow, Recovery)

Attack Editor Tools. Use Unity ScriptableObject to create and assign AttackData.


All commands shall be disabled during Knockback State. State shall be at least half a second long.

Knockback Editor Tools. Use Unity ScriptableObject. Assigned to AttackData and will have one global DeathKnockback Data.
