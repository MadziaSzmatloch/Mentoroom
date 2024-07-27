
# Mentoroom

Mentoroom is a web application created as part of a university assignment, designed for managing university courses. The purpose of the application is to make it easier for professors and students to track university assignments. The main feature of this project is sending files, which are stored on Azure Blob Storage.

## 
I developed this project with a colleague, and I was mainly responsible for the backend of the application, which was written in .NET. The API is published on the Azure platform.

## Architecture
I applied Clean Architecture principles to enhance code organization, used the Mediator pattern, and implemented CQRS using the MediatR library. I also used the Repository Pattern to mediate between the domain and data layers. The database was built using Entity Framework and is also stored on Azure.

## Features
The purpose of this application is for lecturers to be able to create a course that corresponds with a real university course. Lecturers can create assignments in the course, set deadlines, and specify the required files. Students can add themselves to courses, see what files they need to send, and know when to send them.


## Screenshots

### Hero Page
![hero](https://github.com/user-attachments/assets/c1530da3-2402-4f44-a142-d109294b2936)

### Register Page
![register](https://github.com/user-attachments/assets/99dd607a-0b04-4ed1-a910-f4070a665c06)

### Course List
![courses](https://github.com/user-attachments/assets/06aeccdb-f58e-4d16-b62e-5185b78ee950)

### Creating a course
![coursecreate](https://github.com/user-attachments/assets/7837ae6f-3ab7-4376-b174-7a31d0db5001)

### Creating an assignment
![assignmentcreate](https://github.com/user-attachments/assets/2036e758-ff53-4be5-9cf2-013d2a82538f)

### Student List
![studentList](https://github.com/user-attachments/assets/ec413fa0-0c07-4afd-b904-ca90c6005d2f)

### Assignment View
![assignment](https://github.com/user-attachments/assets/06464afc-c2b8-411a-b41c-9aabd591c588)

### Assignment view after sending file
![assignmentsended](https://github.com/user-attachments/assets/c7e9fe58-d5d9-4af7-a9e9-8f012dd5c854)









