# Non-Relational-Database
## Task 1
> -  ### Social Network
> -  **Deadline:** Apr 13, 2020
> -  **Uploading date:** Apr 18, 2020
> -  **Link:** <https://github.com/AnastasiaChernikova/Non-Relational-Database>

> -  **Database:** [MongoDB](https://www.mongodb.com/) 
> -  **Database dump:** <https://github.com/AnastasiaChernikova/Non-Relational-Database/blob/master/damp.txt>

> -  **About:** 
My social network is called Kunst.
In German, this word means "art."
The idea is to create a social network where artists can communicate with each other, watch for achievements and seek inspiration.
The paradox is that here you can watch as Claude Monet disputes with Jan Vermer, and Van Gogh releases a new collaboration with Leonardo da Vinci. Everything is possible in this social network and dreams coming true

> - **Technology:**
ASP.NET , AutoMapper , Bootstrap , MVC pattern , NLog

> - **What requirements have I fulfilled:**
- 5 entities : artist, follower, post, reaction, comment
- you can see stream (posts from other artists)
- you can search people and add new friends
- you can send a reaction for post and comment it

> - **Features:**
- you can edit information about yourself
- you can add you can add an icon which representing you
- you can see top of "the best artist" 
- you can listen "the best song today"
- you can also get fun and play game

> - **Future plans and intentions:**
- it is planned that the rating of the best artist will be formed on the basis of the number of reactions
- add artworks
- add more personal information about artist
- create a bot that will replace artists who have already died
- change design

> - **Project structure:**
- [Kunst.DataAccessLayer](https://github.com/AnastasiaChernikova/Non-Relational-Database/tree/master/Kunst.DataAccessLayer): here you can find Connection Manager, Entities and View Models
- [Kunst.BusinessLogicLayer](https://github.com/AnastasiaChernikova/Non-Relational-Database/tree/master/Kunst.BusinessLogicLayer): here you can find Interfaces and Services
- [Kunst](https://github.com/AnastasiaChernikova/Non-Relational-Database/tree/master/Kunst): here you can find Controllers, Views and StartUp file

## Task 2
> -  ### Social Network
> -  **Deadline:** Apr 18, 2020
> -  **Uploading date:** Apr 18, 2020
> -  **Link:** <https://github.com/AnastasiaChernikova/Non-Relational-Database/tree/master/Kunst.Neo4JTask>

> -  **Database:** [Neo4j](https://neo4j.com/) 

> -  **About:** 
In this task I need to work with the database Neo4j. I should create adding layers over my project (Task 1) and show for my artist common friends. In my generic repository with a Neo4j implementation built on Neo4jClient. Here you can easily build up generic repositories that can be used for CRUD operations on application data model. The library is built on the Neo4jClient which supports the Cypher syntax and building queries using Linq expressions.

> - **Test files:** I add [Social.cs](https://github.com/AnastasiaChernikova/Non-Relational-Database/blob/master/Social.cs),  [NetworkManagment.cs](https://github.com/AnastasiaChernikova/Non-Relational-Database/blob/master/NetworkManagment.cs) and [FraudDetaction.cs](https://github.com/AnastasiaChernikova/Non-Relational-Database/blob/master/FraudDetaction.cs) files which i try to understand and find [tutorial Test-Drive Neo4j with Cypher](https://neo4j.com/developer/#cs-1). Also I create some Neo4j queries for Artist - [ArtistNeo4j.txt](https://github.com/AnastasiaChernikova/Non-Relational-Database/blob/master/ArtistNeo4j.txt)
**My Graph model for Artists**
![image](https://github.com/AnastasiaChernikova/Non-Relational-Database/blob/master/graphArtist.jpg)
**Artists and their Following**
![image](https://github.com/AnastasiaChernikova/Non-Relational-Database/blob/master/tableOfFollowing.jpg)
