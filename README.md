# UserRegistrationMVC5

1. ASP.NET MVC 5 application (front-end: js, bootstrap, html/razor, css, back-end: c#, SQL Server LocalDB).
2. Implemented basic CRUD operations.
3. Used SendGrid(cloud-based delivery platform) for email sending.
4. Used RabbitMQ(messaging broker) for sharing messages during CRUD operations.
5. Used Docker for RabbitMQ img.
6. Used NUnit for writing unit tests.



# Start app (dependencies):
1. Start RabbitMQ and UserRegistrationMVC5 app in the same time, and RabbitMQ service should be running
2. Change email reciver in app.config of RabbitMQ app
3. The email will be send on registration of a new user

