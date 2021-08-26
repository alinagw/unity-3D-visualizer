# Unity Interactive 3D Visualizer

Visualize and interact with different 3D statues in a garden environment. Choose from multiple different statue models, transform them in the space, and customize the materials and environment. Built in Unity using the Universal Render Pipeline.

## About

### Statue Garden

Welcome to your new statue garden! You've recently acquired some very famous and beautiful classic statues. Don't ask how they came to be in your hands, that's not important (just don't mention anything to the Louvre). But now that you have them, it's time to figure out where to put them. Lucky for you, you have a beautiful garden space that has been sitting empty for some time, just waiting for you to add some decorations. There are many popular classical statue and sculpture gardens around the world (Versailles, The Huntington Gardens, etc.) to inspire your garden, but in order to join the ranks of such famous gardens, you have to arrange everything *perfectly*.

Now you can! This app allows you to visualize statues from your collection within your garden, complete with many customization options. Not only can you choose from multiple statues, but you can also apply different materials to each statue. If marble doesn't fit the space, why not try gold, granite, or even a topiary? Once you're satisfied with the look of your statue, you can move it around the garden, rotate it, and even scale it Thankfully science is advanced enough to allow you to transform the statue's material and size with ease. With all these options just a click away, you have all the tools you need to plan out your perfect garden.

Don't forget, however, that the experience of the statues in your garden largely depends on the environment. So why not try visualizing your garden during different times of the day or under different lighting conditions? Luckily you can view your garden in multiple environments to ensure a consistently lovely garden experience! 

### About the Theme

The assignment description mentioned that this was a chance to show off your skills, focused on functionality and elegance in implementation. My strength may be a little different from perhaps what you expected: storytelling. For me, it's always important to create a delightful experience that stands out from the crowd and allows you to actually *enjoy* using the app. I didn't just want to make a standard mesh visualizer where you could view a model in empty space, I wanted to give you a purpose. So I focused on building a story for this assignment in addition to the functionality, to highlight my skills.

Given that the models are rendered in a simulated real-world space, the model transformation functionality is slightly more limited. You mentioned wanting the ability to rotate, scale, and translate the model. All of these transformations are available, but they don't support transformations along every axis. You can translate the model along the X and Z axes, rotate around the Y axis, and uniformly scale the model between a set minimum and maximum value. I made a creative decision to implement a slightly more limited transformation functionality in service of the world-building, hopefully this was an acceptable assumption!

I hope you enjoy!

## Instructions

### How to Play

1. Open project in Unity Editor
2. Ensure that the `3DVisualizer` scene is open
3. In the `Game` tab in the Unity editor, open the aspect dropdown (probably starts off with "Free Aspect" selected) and uncheck "Low Resolution Aspect Ratios" if it is checked (so the graphics look better!). This is only necessary if playing the app in the Unity editor.
4. Click the play button in the Unity editor OR build and run the app

### Building App

I've successfully built and tested the app on the following platforms:

* Mac OS (MacBook Pro)
* iOS 14 (iPhone 12 Pro)
  * *Note: plays in landscape mode only* 
  * *Another note: there's a small UI quirk due to adjusting the UI to the iPhone safe area. I want to fix this in the future.*

I haven't built/tested on Windows, Android, iPad OS, or WebGL.
### App Interface

#### Joystick

In the bottom right-hand corner of the interface is a joystick UI. This joystick is used to transform the model. Depending on the active mode of transformation, the joystick background will be round or oblong with a round handle in the center. Clicking and dragging the handle within the bounds of the joystick background will transform the model.

#### Menu

The **menu** is a white sidebar on the left-hand side of the interface. It starts off collapsed with just icons visible. The top four icons are the **customization tabs** used to toggle between tab panes with lists of different options to choose from to customize the scene. The bottom four icons are the **transform tabs** used to toggle between different modes of transforming the active model.

##### Customization Tabs

On click, these will open/close the menu and switch the active tab pane. Each tab pane has a list of options to choose from. Click again on the currently active tab to close the menu.

1. **Models:** change the active model visualized in the scene
   1. Angel
   2. Apollo
   3. Horse
   4. Lion Crushing Serpent
   5. Mercury
   6. Venus
2. **Materials:** change the active model's material
   1. Aluminum
   2. Brass
   3. Copper (oxidized)
   4. Copper
   5. Glass
   6. Gold
   7. Granite (polished)
   8. Granite (rough)
   9. Marble (polished)
   10. Marble (rough)
   11. Plaster
   12. Steel
   13. Topiary
3. **Times of Day:** change the environment's time of day (skybox, lighting, post-processing volume)
   1. Day
   2. Night
4. **Lights:** toggle on/off different lighting effects
   1. String Lights
   2. Fireflies (particle effect)

##### Transform Tabs

On click, these will *not* affect the open/closed state of the menu. They do not have a corresponding tab pane. However, they will modify the appearance of the joystick used to transform the model.

1. **Translate:** translate the model along X and Z axes with vertical and horizontal joystick
2. **Rotate:** rotate the model around Y axis with horizontal joystick
3. **Scale:** scale the model uniformly with vertical joystick
4. **Reset:** resets the position, rotation, and scale of the model back to the initial state (*not* a tab)

### Scripts

* `OptionsManager.cs`: handles loading and initializing all of the resources used for customization options, and handles triggering the option state update
* `ModelController.cs`: handles transforming (translate, rotate, scale) the active model, updating the model, and updating the model's material
* `MenuManager.cs`: handles generating the menu UI with the customization menu tabs, transformation mode tabs, and option toggles
* `EnvironmentController.cs`: handles updating the time of day (sets skybox, environment lighting gradient colors, global volume, directional light)
* `FirefliesController.cs`: handles toggling the fireflies particle effect on/off
* `StringLightsController.cs`: handles toggling the string lights on/off
* `Option.cs`: stores an option's name and item of specified type (used for models and materials)
* `TimeOfDay.cs`: stores the necessary resources for each time of day option
* `ToggleIconSet.cs`: stores the active and inactive sprite set for a tab button
* `Helper.cs`: utility functions


## Challenges

### Greatest Challenge

The greatest challenge I faced was... the entire project. Unfortunately, I haven't used Unity or coded in C# in a couple of years, so I was painfully out of practice. When I got started working in Unity again, it all felt very familiar but ever so slightly out of reach. When I would get stuck, I often had the thought of "I *used* to know how to do this." I even taught other people how to use Unity a few years ago. Needless to say, I had to relearn a LOT as I worked on this project. It was especially difficult at times since things have changed in Unity since I last used it, particularly when it came to using the URP, so I also had to unlearn some of my old habits as well. But since I had the past background in Unity and C#, it was fairly natural to pick up again. 

I read a ton of documentation. Honestly, I spent most of my time working on this assignment just reading documentation. The Unity editor docs, scripting API, Unity community forums, StackOverflow... I read every resource I could find to answer questions that arose. All good programmers read documentation :)

However, something that documentation couldn't help me with was the ability to "think in Unity". My current career is in web development, which is *very* different from developing in Unity. Before I started my current position at Disney, I didn't know any React despite it being the frontend framework my team uses in our projects. I came from a fairly strong Vue background, but I had to learn React on the job. It was a surprisingly easy transition because I really only had to focus on learning syntax, JSX, React hooks, etc. What I already had from Vue was the ability to think in a front-end framework aka "think in React". I had a strong grasp on building reusable web components, handling state, the reactive lifecycle, web app file structure, communicating between the front-end and back-end... React was a new tool but not a new mindset.

Now, re-entering Unity was a complete shift of mindset. Javascript is not an OOP language in the same way that C# is where everything lives in classes, nor is it a typed language (unless you use Typescript). And scripts attach to GameObjects, which is unique to Unity. Shifting my mindset and figuring out how to split up classes, methods, state, etc. into different scripts for different GameObjects was difficult. I definitely found myself falling into the trap of one script to rule them all rather than spreading it out across GameObjects, and having each object's script pertain to that object. I spent some time experimenting, refactoring code, and trying out different classes. I'm not sure if I ended up with too few or too many scripts in my final product. This was a challenge where I tried my best to recall and read up on the Unity and C# scripting best practices, but without additional practice, I am not positive I can say I "overcame" this challenge per se. Definitely something to keep working on!

### Other Challenges

#### URP

I hadn't heard of the Universal Rendering Pipeline before seeing it in the assignment requirements. I spent extra time reading docs about the URP, how to customize the asset, the post-processing effects, etc. One thing that was particularly difficult was that I was familiar only with the standard material shaders, so I had to spend time remapping any imported materials and playing around with the different settings to see how the new URP shaders worked.

#### Lighting

Lighting in general is kind of my weak spot. I wanted to try out some challenging lighting effects, like changing the environment lighting, toggling lights on/off, etc. But it was definitely a challenge!

When implementing the toggles for the times of day, I realized that changing the skybox during runtime didn't update the ambient lighting/environment reflections using the new skybox. It looked pretty silly to still have light blue lighting when changing to the night skybox. I did some reading, and it seemed like it may be possible to force the lighting to regenerate, but that would be a very performance expensive operation. As a result, I ended up settling for the ambient light set via a gradient instead of the skybox. That way, I could select custom HDR colors for each time of day and change them during runtime. Changing those colors did indeed updating the lighting, so it achieved my goal. It was a little more manual since I had to choose my own colors instead of relying on the skybox, but it was worth it!

Speaking of HDR colors, I didn't realize that creating a `Color` object with rbg values didn't translate to an HDR color which includes intensity as well. I couldn't figure out how to set the intensity of a color or adjust the rbg values to incorporate the intensity. I definitely want to read about this more later, but for now I ended up using handy dandy HDR color pickers for my colors instead!

Along the same lines of updating the lighting on time of day change, I noticed that the particularly metallic statue materials sometimes looked too dark when switching to night mode. At first, I thought it might be the materials themselves, so I played around with the materials' settings with no luck. I later realized that even though I had gotten the ambient light to update, the environment reflections weren't updating. Again, after some reading, reflection probes seemed to be the answer. I had to figure out the appropriate balance of updating the reflection probe without too many performance-heavy re-renders, so I ended up going the scripting route. For some of the materials, there is a *very* small delay between when you toggle the time of day and when the probe finishes rendering, so you may notice that the materials don't update right away. I would love to figure out how to fix this in the future.

#### Scripting

As I mentioned earlier, Javascript is not a typed language. I have used Typescript in the past, but I use JS at my job. It's been some time since I've coded in a typed language, so I definitely ran into trouble with figuring out what types to use in C#. I learned about generic typing using `T`, but there were cases where that didn't work for me (e.g. accessing a property that `T` doesn't have even though the type I'm using does, casting `T`, etc.). I want to learn better C# typing practices in the future.

#### Platforms

Building for iOS proved way more difficult than expected. I thought it would be an easy port over to iOS, but I ended up with several Null Exception errors that I couldn't figure out initially. I built the app again as a development build to get more detailed error messages. After some Google searching, I discovered that building for iOS doesn't support use of the `dynamic` type. I had used the all-inclusive `dynamic` type in a few places where I had some trouble with typing, but I was forced to fix it since it wasn't compatible with iOS. I dove deeper into typing again and managed to make use of generics more!

## Future Considerations

Since this project was quite a relearning experience for me, I definitely took note of many things I would have wanted to implement and/or improve had I more time and experience.

* I wanted to add additional cameras and implement a camera controller. It would have been fun to change the camera view and get to "travel" around the garden. Or maybe even implement a first-person controller to walk around your garden. I have done these things in the past, but I didn't get the chance to focus on it in this project.
* Performance! This is definitely the area with the biggest room for improvement. I did a lot of reading on optimizing performance, so I know there are things that can be better.
  * The models I'm using have pretty complex meshes with many polygons. It may be faster to instantiate all of the models as children of the `Model Controller` from the start and enable/disable the GameObjects rather than instantiating/destroying one at a time (my current implementation).
  * I'm using a lot of point lights for the string lights. Before I was actually using *way* more, but I learned point lights are super not great for performance since they have 6 shadow maps. And they are especially performance heavy when their shadows overlap. I ended up lowering the resolution of the point light shadows and removing many of the point lights, but there is still much room for improvement.
  * I would want to strike a better balance between baked and real-time lighting if possible. Since I'm updating the time of day in real-time, I ended up having most of my lighting update in real-time rather than baked. I would want to learn more about what lighting could be baked and how to save on performance.
* I thought a lot about the UI for transforming the model. I liked the idea of a joystick, since it reminds me of games like Animal Crossing where you use a physical D-pad or joystick to move furniture around. However, I would like to play around with different UI/UX options for the model transformations. 
  * Perhaps using the mouse to click and drag anywhere on the screen
  * Have a UI that supports translating, rotating, and scaling all in one that doesn't require the user to toggle between the modes
  * Currently the transform mode toggles are on the left while the joystick is on the right. For a mobile view in landscape mode, this makes sense so your left thumb can reach the toggles while the right thumb stays fixed on the joystick. Relocating the toggles so the UI is more universal would be something to consider. I thought about having the joystick handle be the toggle through double clicks, but that came with its own challenges. There are many different designs to explore!
  * I would want to make the menu less intrusive so it doesn't cover the model when the customization menu is open. Reducing the opacity is one solution.
 

## Conclusions

I hope you enjoyed building your statue garden! While I know this assignment is not technically flawless, I do hope it provides a good picture of my storytelling and design skills as well as the creative fun I love to have with code. Most importantly, I hope it conveys my ability to learn (re-learn) quickly on the job. My Unity and C# skills were extremely rusty, but I'm pretty proud of what I was able to re-teach myself and accomplish for this project. I've acknowledged a lot of room for improvement, but I am definitely confident that I could continue to sharpen my technical skills and improve my work. 

I definitely want to thank you for giving me the opportunity to jump back into Unity again after so long! I realized just how much I missed it, and I'm excited to use it for some new personal projects.

***Note:** Thank you very much for your patience and understanding while waiting for my assignment submission. I am very sorry for the delay in getting it to you for review. Unfortunately, dealing with a very sudden and unexpected personal emergency took away much of my time, and I wasn't able to get the finishing touches and write-up ready for you earlier. I appreciate your willingness to still consider my assignment for review. Thank you so much!*
