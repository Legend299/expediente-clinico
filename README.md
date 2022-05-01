# Expediente clínico

## Problema

La Secretaría de Salud en México solicita a una empresa de desarrollo de software que realice el diseño arquitectónico de un Expediente Clínico Electrónico que sea único para los habitantes de México. En la solicitud se especifica que el expediente podrá ser utilizado tanto en los hospitales públicos como en los privados, así como también podrán utilizarse con los médicos y laboratorios químicos particulares.

Entre las consideraciones que se han solicitado están:

1.	Cada uno de los habitantes podrá portar su expediente y siempre y cuando sea mayor de edad, de tal manera que éste permitirá o no el acceso a los trabajadores de la salud para agregar y/o consultar la información contenida en el expediente. Para los menores de edad tiene que existir un tutor que sea el responsable de otorgar el acceso.

2.	Solo los trabajadores de salud con cédula profesional tendrán acceso a los expedientes de los pacientes y su acceso serán mediante su huella dactilar.

3.	El dueño del expediente permitirá los accesos a su expediente mediante su huella dactilar u otro dato biométrico.

4.	El expediente debe permitir el ingreso y consulta a imágenes (p.ej. radiografías),
información de textos (p.ej. información de consultas médicas), y documentos PDF (p.ej. resultados de laboratorio).

Al momento de realizar una cita médica, la información del expediente debe estar disponible previamente a su consulta, por lo que deberá ser posible otorgar a distancia el acceso al expediente clínico mediante la huella dactilar. Solo se otorgará el acceso al médico con quien se pactó la cita.


## Herramientas

### Cliente Web
   - .NET 6
     - C# 10
     - Razor
     - Entity Framework 6 para MySql
   - HTML5, CSS, JavaScript
     - Bootstrap 4.3.1
     - Datatable 1.11.5
     - JQuery 3.5.1
     - Ajax

Dependencias para instalar desde la consola Nugget:
```
Install-Package DateOnlyTimeOnly.AspNet -Version 1.0.2
Install-Package Newtonsoft.Json -Version 13.0.1
Install-Package Microsoft.EntityFrameworkCore -Version 6.0.4
Install-Package Microsoft.EntityFrameworkCore.Design -Version 6.0.4
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 6.0.4
Install-Package Pomelo.EntityFrameworkCore.MySql -Version 6.0.1
Install-Package RabbitMQ.Client -Version 6.2.4
```

#### Seguridad

Json-Web-Token
```
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 6.0.4
Install-Package System.IdentityModel.Tokens.Jwt -Version 6.17.0
```

Para instalar depedencias:

![image](https://user-images.githubusercontent.com/64448236/164333777-ebfe2168-9cfb-4b99-9c1e-15baca9be9a0.png)

![image](https://user-images.githubusercontent.com/64448236/164334273-e11863bd-e7f4-406c-bf5a-f56de2277c58.png)


### Cliente Movil
   - Java
   - Diseño XML

Dependencias:
```
 implementation 'com.android.volley:volley:1.2.0'
```

### Base de Datos
   - XAMPP 3.3.0
     - Apache
     - MySql 10.4.22-MariaDB

Dependencias:
```
mysql-connector-net-8.0.18
mysql-for-visualstudio-1.2.9
```

## Diagrama de despliegue

![diagrama_despliegue_expediente_médico drawio](https://user-images.githubusercontent.com/64448236/163930802-ef71555c-9ed3-4dd4-a50e-75056e11f6e4.png)

## Diagrama de componentes

![DiagramaComponentes drawio (2)](https://user-images.githubusercontent.com/64448236/163931202-c0e3233a-9edf-476e-8975-d3288725c8d4.png)
