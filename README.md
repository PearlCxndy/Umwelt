# Final Project Documentation: Umwelt

---

## Project Description

The project aims to reflect non-human organisms sensory experiences of world, through an interactive digital installation. The project name is the term, "Umwelt" that has been mentioned in the book *An Immense World* by Ed Yong. This termtranslates to "environment" or "surroundings" but also connotes the particular perspective of a particular organism, shaped by its sensory organs capabilities and perceptual systems. This project aims to make the user to asks philosophical questions such as "How does is my perception is shaping the way I think" and ethical questions as "Is the ideal world I am imagining for myself is also ideal for another species". The project aims to help the user locate themseles (as humans) within the world, equally with all of their neighbours that they are sharing this world. 

---
## Technical Description

The project is an interactive, projection mapped, revolving cube. A digital environment of four different animals' (Bird, Dog, Octupus, Bat) visions, are reflected as a game environment (made with Unity) on each vertical face. The revolving happens manually and an autoencoder reads the rotation data and sends it into Unity and then to Touhdesigner (for projection mapping), consecutivley. 

The interaction happens as the diagram below, where the user spins the cube to explore the sensory visions of four different animals  and can enter into the scene and navigate in a  playable way.

![image](https://github.com/user-attachments/assets/6f8c7ca7-5303-4598-86ab-dc3d2b6b9617)

**Technical Equipment**
  
<div align="center">

<table width="80%">
  <tr>
    <td width="50%" valign="top">
      <h3>Hardware</h3>
      <ul>
        <li>Cardboard box</li>
        <li>Rotary encoder</li>
        <li>Arduino Uno</li>
        <li>Breadboard</li>
      </ul>
    </td>
    <td width="50%" valign="top">
      <h3>Software</h3>
      <ul>
        <li>TouchDesigner</li>
        <li>Unity</li>
        <li>GitHub Desktop</li>
        <li>Arduino IDE</li>
        <li>Visual Studio Code</li>
        <li>Fusion 360</li>
        <li>Rhino Software</li>
        <li>Blender</li>
      </ul>
    </td>
  </tr>
</table>

</div>



  ![pictures]()

## Proccess

**1. Ideation**</br>

During our group brainstorming, we were inspired by how dogs can smell traces of the past—movements and events in a space. This sparked our initial idea to explore time perception in humans vs. non-human animals. As we dug into references, we gradually narrowed the scope, eventually focusing on environmental and urban concepts shaped by animal perception.

**Challenges and Refinements**
**Issue:** We originally planned to explore time perception, but due to limitations in Unity gameplay, we narrowed the focus to animal perception to create a more immersive experience. Some larger ideas were dropped due to budget constraints, and CCI couldn’t provide resources like a big screen or interactive space.
**Solution:** To manage the project effectively, we used tools like Notion and Figma. With a team of four, communication could have been difficult, but Notion helped us assign and adjust tasks easily (see Figure 1 for our time estimates and task breakdown). Each subject was clearly separated for better time and workload management

We also used notebooks occasionally to sketch ideas, but Figma was our main collaboration tool. It allowed us to visualize and draw in real time, making it easier to share ideas and stay aligned throughout the project.

![figma&notion]()


**2. Research**</br>

After finalizing our concept, we entered a deep research phase using various sources. Jessica recommended An Immense World by Ed Yong, which explores how animals perceive the world through different senses. We also pulled information from online articles (see bibliography), YouTube videos, Spotify podcasts, and more. 

![reasearch]()

For primary research, we visited the Natural History Museum in London and the National Museum of Scotland. These trips had a big impact—we observed how museums present animal perception through interactive installations. We also recorded rare bird sounds ourselves, since many weren’t available online; these recordings are featured in Ceci Branch’s work.

![Museum]()

**Challenges and Refinements**
**Issue:** We faced time constraints that limited our ability to visit more museums, and some useful articles were behind paywalls. Although we couldn’t finish the entire book Jessica recommended (An Immense World), the specific chapters we did read were valuable.
**Solution:** Despite the limitations, this research phase helped us see how time perception could be explored through multiple sensory experiences. It inspired us to design a range of interactive elements. We focused on gathering detailed information for each animal to make their unique perceptions feel real in the final experience.

**3. Technical and Visual Research**</br>

We explored different technologies and environments to bring our idea to life. Some early concepts included immersive domes and curved screen projections, inspired by projects like this immersive dome and curved screen projection in Unity. Eventually, we chose to use a rotating cube as our main interaction method, as it allowed us to clearly and interactively switch between different animal perspectives (game scenes). The rotating cube idea was inspired by a project we found on YouTube using projection mapping with Unity, Leap Motion, and servo motors. We aimed to create a similarly immersive experience, potentially enhancing it with our own tech setup.

![Figma pics?]()


Moreover, we decided to build the project in Unity, using shaders to reflect each animal’s unique sensory perception. To stay organized and cohesive, we created a visual library with references for environments, characters, shaders, and color palettes. Each of the cube’s four sides represents a different animal, with each team member responsible for designing one scene. These scenes were developed individually, then pushed to GitHub and merged by Pearl

The virtual cube was built in Unity using a multi-camera setup. We followed a portal rendering tutorial to assign each face to a specific scene and the physical cube and turntable underneath were 3D modeled by Ceci and laser-cut for assembly which will explain more later

![Figma pics]()


**Challenges and Refinements**
**Issue:** As well as the limitation of the screen we gonna do the curved screen projection but then the range that we can borrow the projector and to cube is not good quality enough so we decided no to do all of the 3 sides but will put in on the future work tho becuase it's can leads to collab game play as well as the servo motor that when we control by the controller the cube will automatically rotate with out any manual moveement but in the end it's just too much work 
**Solution:** we switched for rotating cube to be just 

**3.Building Each Scene**</br>

After finsihing the ideation and  research phases we moved on to buiding our own individual scenes, where each of us chose one of the animals and created a scene for it.

EVERYONE INDIVIDUALLY

**4.Making the Digital Cube**</br>

PEARL

**Challenges and Refinements**
**Issue:**
**Solution:**

**5. Merging the Digital Cube with the Individual Scenes**</br>

PEARL (Github) 

**Challenges and Refinements**
**Issue:**
**Solution:**

**6. Modeling and Laser Cutting the Revolving Cube**</br>

**Challenges and Refinements**
Ceci have to write that 
**Issue:**
**Solution:**

**7. Projection Mapping**</br>

**Challenges and Refinements**
Bam have to write that 
**Issue:**
**Solution:**
We firstly tried connecting Unity to Madmapper. However, because of the license issues we couldn't use it. We decided to switch to Touchdesigner. Where we used [this video](https://www.youtube.com/watch?v=iIwcqgAPVWI) to connect the Unity Scene to Touchdesigner using [Klak Spout](https://github.com/keijiro/KlakSpout) package. 

## Final Project Images 

---
## Github/ ChatGPT Link
---
## User Testing
---
## Bibliography
---
