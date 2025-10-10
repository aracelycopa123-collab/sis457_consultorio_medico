\# sis457\_consultorio\_medico

Trabajo Final de Laboratorio de Fundamentos de la Programación

1\. Descripción del Consultorio Médico



El consultorio médico es un espacio de atención integral de salud que ofrece servicios especializados en cardiología y odontología. Su objetivo es brindar una atención médica de calidad, personalizada y oportuna,

enfocada en el bienestar de sus pacientes de todas las edades.



El consultorio está dividido en áreas específicas para cada especialidad, contando con infraestructura moderna, equipos adecuados y personal capacitado. Las instalaciones son cómodas, 

limpias y diseñadas para garantizar la seguridad y la privacidad de los pacientes.



Áreas del Consultorio:



1\. Recepción y sala de espera:



Espacio acogedor con sillas cómodas.



Área de recepción donde se realiza la gestión de citas y atención administrativa.







2\. Consultorio de Cardiología:



Equipado con electrocardiógrafo, tensiómetro digital, y estetoscopios especializados.



Se realizan exámenes de rutina, control de presión arterial, chequeos del ritmo cardíaco y orientación para pacientes con enfermedades cardiovasculares.



Atención dirigida por un cardiólogo certificado.







3\. Consultorio de Odontología:



Cuenta con silla odontológica ergonómica, lámpara de alta intensidad, unidad de rayos X intraoral y herramientas de diagnóstico dental.



Se ofrecen limpiezas, tratamientos de caries, extracciones, ortodoncia básica y asesoría en salud bucal.



Atendido por odontólogos titulados y asistentes dentales.





Alcance del Sistema



El sistema que se desarrollará permitirá gestionar de forma ordenada:



Registro y control de pacientes.



Administración de citas médicas por especialidad.



Historial clínico por paciente.



Registro del personal médico (odontólogos y cardiólogos).





Este proyecto está pensado para mejorar la eficiencia del consultorio, reducir el uso de papel, minimizar errores en la gestión clínica y brindar una experiencia más cómoda y rápida tanto para pacientes como para el personal de salud.





-----



2\. Entidades tentativas

1\. Usuario

\- id

\- idDoctor

\- usuario

\- clave

\- usuarioRegistro

\- fechaRegistro

\- estado



---



2\. Paciente

\- id

\- ci

\- nombre

\- apellidoPaterno

\- apellidoMaterno

\- fechaNacimiento

\- celular

\- direccion

\- usuarioRegistro

\- fechaRegistro

\- estado



----



3\. Doctor

\- id

\- ci

\- idEspecialidad

\- nombre

\- apellidoPaterno

\- apellidoMaterno

\- direccion

\- celular

\- usuarioRegistro

\- fechaRegistro

\- estado



---



4\. Especialidad

\- id

\- nombre

\- usuarioRegistro

\- fechaRegistro

\- estado



---



5\. Cita

\- id

\- idPaciente

\- idDoctor

\- fecha

\- hora

\- usuarioRegistro

\- fechaRegistro

\- estado



---



6\. Pago

\- id

\- idCita

\- transaccion

\- fechaPago

\- usuarioRegistro

\- fechaRegistro

\- estado



---



7\. HistorialClinico

\- id

\- idPaciente

\- idCita

\- diagnostico

\- tratamiento

\- fecha

\- usuarioRegistro

\- fechaRegistro

\- estado

