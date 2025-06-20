# Final Project Documentation: Umwelt

---

## Project Description

The project's goal is to create an interactive digital installation that mimics the sensory experiences of non-human species. According to Ed Yong's book *An Immense World*, the phrase "Umwelt" is the project name. In addition to meaning "environment" or "surroundings," this phrase also refers to an organism's unique viewpoint, which is influenced by its perceptual systems and sensory organs. This project seeks to provoke the user to consider ethical and philosophical issues, such as "Is the ideal world I am imagining for myself also ideal for another species?" and "How does my perception shape the way I think?" As humans, the project seeks to assist the user in finding their place in the world, on an equal footing with all of their neighbours. 


![13](https://github.com/user-attachments/assets/f3ea9109-a223-432e-a26e-42c026747d2d)

---
## Technical Description

The project is a rotating cube that is interactive and projection mapped. Each vertical face displays a game world created using Unity that reflects the digital environments of four distinct animals: birds, dogs, octopuses, and bats. Rotation is done manually, and the rotation data is read by an autoencoder, which subsequently delivers it to Unity and Touhdesigner (for projection mapping). 

The user can join the scenario and navigate in a playable manner by spinning the cube to explore the sensory views of four distinct animals, as shown in the diagram below.

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



  ![pictures](https://github.com/user-attachments/assets/9ab194ff-7be1-42fa-8daf-0cea1b3f671d)

---
## Proccess

### 1. Ideation

The ability of canines to detect remnants of the past—movements and occurrences in a space—inspired our group brainstorming. This gave rise to our original plan to investigate how humans and non-human animals perceive time. We progressively focused on urban and environmental notions influenced by animal perception as we explored further into references.How can we use immersive technology to depict non-human senses? was the main topic of our investigation. This informed our technological and design decisions. In order to better investigate physical engagement in immersive places, Mahalia advised that we go to the Nerve Lab in Holborn and take a movement session. Even though we were unable to visit, there is still a good chance that it may develop in the future. We tested our concepts at CSM's Art & Science department and got insightful criticism. Mahalia also assisted us in honing the idea by offering ideas for using immersive technology to depict animal senses including sight, smell, and hearing.

![image](https://github.com/user-attachments/assets/eece033d-8cd6-4d99-b65a-f3c79883c07d)



&nbsp;&nbsp;&nbsp;&nbsp;**Challenges and Refinements**</br>

&nbsp;&nbsp;&nbsp;&nbsp;**Issue:** We had originally intended to investigate time perception, but in order to make the experience more immersive, we limited the focus to animal perception because of Unity's gameplay constraints. Due to financial limitations, some larger concepts were abandoned, and CCI was unable to supply resources like an interactive area or a wide screen. In order to support the initiative, we also intended to get in touch with other specialists and partners, but time constraints made this challenging. It would be worthwhile to follow in the future, though.

&nbsp;&nbsp;&nbsp;&nbsp;**Solution:** Notion and Figma were among the tools we utilised to efficiently manage the project. It may have been challenging to communicate as a four-person team, but Notion made it simple to assign and modify tasks (see Figure 1 for our time estimations and work breakdown). Clear divisions between the subjects allowed for improved time and workload management. Although we occasionally drew ideas in notebooks as well, Figma served as our primary platform for cooperation. It made it simpler to exchange ideas and maintain project alignment by enabling us to sketch and visualise in real time.

![figma&notion](https://github.com/user-attachments/assets/f4767422-0624-4379-b478-302d108995e2)


#### 2. Research

Once our concept was complete, we started a thorough research phase using a variety of sources. Ed Yong's An Immense environment, which examines how animals use their senses to comprehend the environment, was suggested by Jessica. Additionally, we gathered data from YouTube videos, Spotify podcasts, internet articles (refer to the bibliography), and more. 

We went to the National Museum of Scotland and the Natural History Museum in London for primary research. These excursions had a significant influence; we saw how interactive exhibits at museums depict animal perception. Since many of the unusual bird sounds weren't accessible online, we also captured them ourselves; Ceci Branch's work includes these recordings.

![image](https://github.com/user-attachments/assets/7678e10e-050d-440f-bb42-ad27dbbca1a0)

&nbsp;&nbsp;&nbsp;&nbsp;**Challenges and Refinements**

&nbsp;&nbsp;&nbsp;&nbsp;**Issue:** We were unable to visit more museums due to time constraints, and other helpful publications were protected by paywalls. The chapters we did read of Jessica's suggested book, An Immense World, were worthwhile even if we were unable to finish it.

&nbsp;&nbsp;&nbsp;&nbsp;**Solution:** Even its shortcomings, this phase of the research helped us to realise how various senses could be applied to probe time perception. It inspired us to produce a range of interactive elements. We focused on gathering thorough knowledge about every animal so that their unique views felt real at the end.

### 3. Technical and Visual Research

We looked at several technologies and configurations to realise our idea. Inspired by projects like Unity's immersive dome and curved screen projection, several early concepts incorporated immersive domes and curved screens. At last, we chose to largely engage utilising a spinning cube since it allowed us clearly and interactively change between many animal viewpoints (game scenarios). We arrived over [this project](https://www.youtube.com/watch?v=oCwE5ayHgjM).) online and derived inspiration from Inspired by a project we came upon on YouTube using Unity, Leap Motion, and servo motors, the spinning cube idea sprang from projection mapping with Unity. Our aim was to create a similarly immersive experience, maybe enhanced by our own technology setup.


We also decided to make the project in Unity using shaders representing the unique sensory experience of every animal. To maintain coherence and order, we created a visual library with references for environments, characters, shaders, and colour palleties. Every team member is in charge of creating one scenario, and each of the four sides of the cube shows an other animal. These scenes evolved independently, then merged by Pearl after being pushed to GitHub.

Unity constructed the virtual cube under a multi-camera configuration. Using a gateway rendering tutorial, we allocated each face to a certain scene; the physical cube and turntable beneath were 3D designed by Ceci and laser-cut for assembly, which will be covered further later.


&nbsp;&nbsp;&nbsp;&nbsp;**Challenges and Refinements**

&nbsp;&nbsp;&nbsp;&nbsp;**Issue:** Though the current projectors lacked the quality and range required, we had intended to use curved screen projection and display on all three sides of the cube. We also thought of auto-rotating the cube using a servo motor via controller input, but it turned out too difficult and time-consuming.

&nbsp;&nbsp;&nbsp;&nbsp;**Solution:** Time and technical constraints drove us to scale down to a single-side projection. Lieven proposed a configuration whereby we could display onto three sides, but it needed a higher-mounted projector and more work than we could have handled in our available time. Future expansion will take this concept into consideration.

### 3.Building Each Scene</br>

After finsihing the ideation and  research phases we moved on to buiding our own individual scenes, where each of us chose one of the animals and created a scene for it. The processes are explained in detail in the Individual Contributions section.

### 4.Making the Digital Cube</br>

We first constructed a virtual replica in Unity to see how the finished cube may appear and operate before building the actual one. This let us test concepts, play about with camera configurations, and learn how each scene might be shown interactively. Originally, we wanted to apply projection mapping on a revolving cube and mix all animal sequences. But syncing scenes, cube movement, and projection mapping brought a lot of complexity that called for a change of direction.

&nbsp;&nbsp;&nbsp;&nbsp;**Challenges and Refinements**

&nbsp;&nbsp;&nbsp;&nbsp;**Issue:** We struggled how to maintain every scene dynamic while projecting maps onto a revolving cube. Our original scheme called for rotating all scenes utilising hardware and programs. We tested several input techniques—mouse drag, arrow keys, encoder with Arduino—but coordinating camera angles, cube rotation, and scene transitions proved challenging—especially since every scene had distinct camera distances and prefab setups.

&nbsp;&nbsp;&nbsp;&nbsp;**Solution:** We discovered [a tutorial on portal cameras](https://www.youtube.com/watch?v=VituktEvIY8), It provided a simpler, more efficient means of replicating every scenario on several sides of the cube. We group five Unity cameras—one primary and one for each cube face—into a single spinning GameObject. We first used automatic rotation, then moved to mouse drag, arrow key input, and then Arduino included a rotational encoder. Serial data sent by the encoder into Unity sets Unity into rotation.
We developed a decoy preview system—akin to a character selection screen—to manage scene transitions so users could rotate the cube and see various animal habitats. We also created a custom script adapting for various scene layouts by syncing camera orientation with cube rotation. Future projection mapping integration was put in motion by this approach, which also made the experience more participatory and immersive.

![digital cube](https://github.com/user-attachments/assets/c02ee90b-9308-4d7c-9fb9-3f44d70e198f)


### 5. Merging the Digital Cube with the Individual Scenes</br>

Every team member worked on unique Unity sceneries connected to distinct cube faces to create our interactive experience. We constructed a virtual version in Unity to see the finished cube and test the interaction before construction of the actual cube started. The idea was to spin a digital cube linking each face to a distinct animal picture, therefore producing a smooth, immersive change. Like a character choosing screen, we also created a preview scene so users may explore settings before starting the real gaming.

&nbsp;&nbsp;&nbsp;&nbsp;**Challenges and Refinements**

&nbsp;&nbsp;&nbsp;&nbsp;**Issue:** The primary difficulty arose in trying to combine every scene into a single workable Unity project. Using many Unity versions caused compatibility problems including broken shaders and conflicts between Burst versions. Prefab linkages and supplies were sometimes lost during merger, needing hand reassignment. Especially with Unity metadata and `.DS_Store` files, merge conflicts created numerous problems and prompted us to often revert and restart branches. Additionally upsetting the visual flow were uneven lighting and skyboxes between scenes. Standardising became difficult also because the gameplay viewpoints differed: octopus and dog utilised third-person, while bat and bird used first-person.

&nbsp;&nbsp;&nbsp;&nbsp;**Solution:** We standardised the Unity version on every device to stop more compatibility issues. Mass material application to a few selected objects helped to more precisely distribute shaders updated for a few selected components. Regular backups, frequent commits, and thorough debugging assisted merging issues to be resolved. We targeted at scene consistency using portal cameras and arranging them into a spinning cube object coordinated with an Arduino rotary encoder. We built a sphere around each camera and placed shaders within to duplicate scene environments, so addressing lighting variations. At last, daily team meetings and open communication helped us to quickly resolve problems, match our procedures, and properly unite all components into one interactive system.

![github](https://github.com/user-attachments/assets/2cdebac3-5c97-42ba-aad4-b1c34033e522)

### 6. Modeling and Laser Cutting the Revolving Cube</br>

We developed laser-cut files using Fusion 360, then extruded them into 3D to show the complete assembly after creating 2D drawings of the cube's surfaces. Seeing the digital model provided us confidence going into production and enabled us to know how the elements will fit.

We tested the turntable early on using cardboard and marbles. The marbles proved unsatisfactory; they were erratic and inconsistent. Changing to metal ball bearings greatly enhanced rotation, therefore smoothing out the machine and increasing dependability. Laser-cut acrylic, which we also used instead of cardboard, provided superior durability, accuracy, and a smoother finish for the framework. From rough prototype to a working, polished build, this update represented a definite step ahead.

&nbsp;&nbsp;&nbsp;&nbsp;**Challenges and Refinements**

&nbsp;&nbsp;&nbsp;&nbsp;**Issue:** Designing the turntable involved several tough choices. We were short on time and the fabrication lab was full during finals, hence we wanted something quick to construct, structurally sound, but without using 3D printing. Our aim was to keep using laser cutting and layer materials to create a working basis. Surprisingly entertaining and low-stress, we began experimenting with cardboard and marbles to test the bearing mechanism. Moving into acrylic and metal bearings, however, brought increased precise demands and challenging alignment issues.Technically, the Arduino and Unity Serial Port connectivity caused us much trouble. Values were first erratic and we lacked reliable rotation input. That was annoying since we knew that this one hurdle was essential for syncing the physical and digital cubes and felt as though everything was held up by it. Syncing the speed between the real and virtual cubes also introduced still another level of difficulty. It was difficult when things did not match our expectations, particularly considering the effort both sides had put in.

&nbsp;&nbsp;&nbsp;&nbsp;**Solution:** Designing the turntable involved several tough choices. We were short on time and the fabrication lab was full during finals, hence we wanted something quick to construct, structurally sound, but without using 3D printing. Our aim was to keep using laser cutting and layer materials to create a working basis. Surprisingly entertaining and low-stress, we began experimenting with cardboard and marbles to test the bearing mechanism. Moving into acrylic and metal bearings, however, brought increased precise demands and challenging alignment issues.Technically, the Arduino and Unity Serial Port connectivity caused us much trouble. Values were first erratic and we lacked reliable rotation input. That was annoying since we knew that this one hurdle was essential for syncing the physical and digital cubes and felt as though everything was held up by it. Syncing the speed between the real and virtual cubes also introduced still another level of difficulty. It was difficult when things did not match our expectations, particularly considering the effort both sides had put in.

![progress](https://github.com/user-attachments/assets/6431ac35-be8b-44fe-8cac-54a93211e377)


### 7. Projection Mapping</br>

We tried MadMapper to do the projection mapping. The setup went perfectly but since the MadMapper trial version adds a watermark, we planned to get a license from CCI for usage during our final playtest. Howeve

To improve the immersive experience, we intended to project images onto a physical box we built. First, we looked at ways to link Unity to projection tools and came upon [**KlakSyphon**](https://github.com/keijiro/KlakSyphon?tab=readme-ov-file), and effectively tested it using MadMapper. The setup went perfectly, 

&nbsp;&nbsp;&nbsp;&nbsp;**Challenges and Refinements**

&nbsp;&nbsp;&nbsp;&nbsp;**Issue:** Given our quite simple projection needs, Lieven from the DarkLab advised looking at free options like MapMap or TouchDesigner after consulting him. But we soon discovered MapMap was no longer supported and unable to find a trustworthy download. We turned to TouchDesigner, which first worked utilising the **SyphonSpout In TOP** to get the Unity feed. The problems started when we mapped using **KantanMapper**. Although it worked the first time, restarting the Unity scene usually caused the projection output in KantanMapper to glitch, fade, or vanish totally, therefore compromising the setup.

  &nbsp;&nbsp;&nbsp;&nbsp;**Solution:** We used another repository by the same developer, [**KlakSpout**]([https://github.com/keijiro/KlakSyphon?tab=readme-ov-file](https://github.com/keijiro/KlakSpout). Also we switched from Mac into a PC. This new solution allowed us to seamlessly make the projection mapping work. 

![Projection Mapping](https://github.com/user-attachments/assets/45f56694-d674-4f83-8e6b-1bb09c831667)

---
## 
---
## Final Project Images 

---
## Github/ ChatGPT Link
---
## User Testing

We prepared a comfortable and informal user testing session based on our previous UX/UI experience. Our process consisted of four steps: a pre-task questionnaire, scenario setting, prototype interaction, and a post-task questionnaire. The primary goals were to examine the interaction design of scene navigation, evaluate object interaction for each animal, and investigate how users' opinions on non-human perception changed throughout the encounter. We disregarded standard performance indicators because we were more concerned with open exploration than precise tasks. Instead, we used basic performance metrics such as time on task and self-reported feedback obtained via a Likert scale questionnaire.

![12](https://github.com/user-attachments/assets/e760b9c0-9356-4b9a-a45c-c48fb1328944)

---
## Bibliography

**Common Resources**
- An Immense World
- Programming for People (2018) Spout for Unity (Texture sharing for other programs) Resolume, Touchdesigner, VVVV. https://www.youtube.com/watch?v=iIwcqgAPVWI.
- Keijiro (no date) GitHub - keijiro/KlakSpout: Spout plugin for Unity. https://github.com/keijiro/KlakSpout.
- pixelasm (2016b) Interactive projection mapping done with Unity 3d, Leap Motion and Arduino / Uniduino / Adafruit. https://www.youtube.com/watch?v=oCwE5ayHgjM.
- Does critical flicker-fusion frequency track the subjective experience of time? - Rethink Priorities (2024b). https://rethinkpriorities.org/research-area/does-critical-flicker-fusion-frequency-track-the-subjective-experience-of-time/.
- Animal senses: how do they perceive the world and what important things can they sense that we cannot? | Gresham College (no date b). https://www.gresham.ac.uk/watch-now/animal-senses-how-do-they-perceive-world-and-what-important-things-can-they.

**technical resources**
- WarriorWork (2016) Do YOU work with multiple scenes at the same time? https://www.reddit.com/r/Unity3D/comments/x0h9m6/do_you_work_with_multiple_scenes_at_the_same_time/ (Accessed: June 19, 2025).
- How to create a “portal” to another scene within my scene? (2020). https://blenderartists.org/t/how-to-create-a-portal-to-another-scene-within-my-scene/1257163 (Accessed: June 19, 2025).
- Is it possible to run two or more scenes in parallel? (2023). https://discussions.unity.com/t/is-it-possible-to-run-two-or-more-scenes-in-parallel/936294.
- Project view from a camera of other scene (2021). https://discussions.unity.com/t/project-view-from-a-camera-of-other-scene/863913/2.
- Unity (2020) How to work with multiple scenes in Unity. https://www.youtube.com/watch?v=zObWVOv1GlE.
  
---
