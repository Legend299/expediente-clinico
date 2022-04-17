package com.expedienteclinico.Controlador;

public enum RolesUsuario {
    ADMINISTRADOR(1),
    MEDICO(2),
    USUARIO(3);

    private final int num;

    RolesUsuario(final int num){
        this.num = num;
    }

    public int get(){
        return num;
    }
}
