package com.expedienteclinico.Controlador;

public class SessionUsuario {

    private static SessionUsuario sessionUsuario = null;

    //Valores usuario
    private int IdUsuario;
    private String Correo;
    private String Password;
    private int IdRol;
    private int IdExpediente;
    private boolean Activo;

    public SessionUsuario(){
    };

    public int getIdUsuario() {
        return IdUsuario;
    }

    public void setIdUsuario(int idUsuario) {
        IdUsuario = idUsuario;
    }

    public String getCorreo() {
        return Correo;
    }

    public void setCorreo(String correo) {
        Correo = correo;
    }

    public String getPassword() {
        return Password;
    }

    public void setPassword(String password) {
        Password = password;
    }

    public int getIdRol() {
        return IdRol;
    }

    public void setIdRol(int idRol) {
        IdRol = idRol;
    }

    public int getIdExpediente() {
        return IdExpediente;
    }

    public void setIdExpediente(int idExpediente) {
        IdExpediente = idExpediente;
    }

    public boolean isActivo() {
        return Activo;
    }

    public void setActivo(boolean activo) {
        Activo = activo;
    }

    public static SessionUsuario getInstance(){
        if(sessionUsuario == null){
            sessionUsuario = new SessionUsuario();
        }
        return (sessionUsuario);
    }

}
