Docker PGAdmin:
 docker pull dpage/pgadmin4 
 docker run --name pgadmin-container -p 5050:80 -e PGADMIN_DEFAULT_EMAIL=user@domain.com -e PGADMIN_DEFAULT_PASSWORD=catsarecool -d dpage/pgadmin4
 Use localhost:5050 this url for logging in.
 Remember to replace “user@domain.com” and “catsarecool” with your email and password.
