# Open Source Software (OSS) Development

These rules come from a talk by Evan Shaw called Open Source Rules.

You can learn a lot from contributing to open source.

Do things for your team that helps them or reduces issues,
like researching approaches or investigating dependencies,
add features to opensource libraries, etc.

Rules:  
1. Investigate / Evaluate your dependencies. Are they needed?
   You have a stake in your own dependecies. If they have issues, you should report or investigate it.
2. When you use an external library and an issue occurs, assume it is always your fault...
   until you can prove that it's not.
   Build small pieces using the external dependency to identify how it operates and whether you are using it correctly.
   If there is an issue then this makes it easier to identify.
3. OSS is people. You don't know what's going on with them and why they take time to respond.
4. Scratch your own itch. If you need an issue fixed or a new feature add, then it might be up to you to do.
5. Nothing is magic. Write code you and others can comprehend.
   The code in a library is written by normal people, not a super genius.
   Chesterton's Fence: don't remove something until you know why it was put there.
6. Leave a trail. Almost impossible to over describe why you did something.
   Useful for your future self as you will like be the one looking at this code.
   
Rule (for maintainers, the repo owner, and contributers)  
State your intentions. Is this going to be a maintained repo, was it a weekend project, or is a big company behind it?

Rule:  
Software is never done. It will continue changing.
