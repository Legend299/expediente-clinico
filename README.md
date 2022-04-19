# expediente clínico

## Problema

La Secretaría de Salud en México solicita a una empresa de desarrollo de software que realice el diseño arquitectónico de un Expediente Clínico Electrónico que sea único para los habitantes de México. En la solicitud se especifica que el expediente podrá ser utilizado tanto en los hospitales públicos como en los privados, así como también podrán utilizarse con los médicos y laboratorios químicos particulares.

Entre las consideraciones que se han solicitado están:

1.	Cada uno de los habitantes podrá portar su expediente y siempre y cuando sea mayor de edad, de tal manera que éste permitirá o no el acceso a los trabajadores de la salud para agregar y/o consultar la información contenida en el expediente. Para los menores de edad tiene que existir un tutor que sea el responsable de otorgar el acceso.

2.	Solo los trabajadores de salud con cédula profesional tendrán acceso a los expedientes de los pacientes y su acceso serán mediante su huella dactilar.

3.	El dueño del expediente permitirá los accesos a su expediente mediante su huella dactilar u otro dato biométrico.

4.	El expediente debe permitir el ingreso y consulta a imágenes (p.ej. radiografías),
información de textos (p.ej. información de consultas médicas), y documentos PDF (p.ej. resultados de laboratorio).

Al momento de realizar una cita médica, la información del expediente debe estar disponible previamente a su consulta, por lo que deberá ser posible otorgar a distancia el acceso al expediente clínico mediante la huella dactilar. Solo se otorgará el acceso al médico con quien se pactó la cita.


## Diagrama de despliegue

![diagrama_despliegue_expediente_médico drawio](https://user-images.githubusercontent.com/64448236/163930802-ef71555c-9ed3-4dd4-a50e-75056e11f6e4.png)

## Diagrama de componentes

![DiagramaComponentes drawio (2)](https://user-images.githubusercontent.com/64448236/163931202-c0e3233a-9edf-476e-8975-d3288725c8d4.png)
