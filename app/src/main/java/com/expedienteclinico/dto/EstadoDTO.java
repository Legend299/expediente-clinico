package com.expedienteclinico.dto;

import java.io.Serializable;

public class EstadoDTO implements Serializable {

    private int IdEstado;
    private String Nombre;

    public EstadoDTO() {
    }

    public EstadoDTO(int idEstado, String nombre) {
        IdEstado = idEstado;
        Nombre = nombre;
    }

    public int getIdEstado() {
        return IdEstado;
    }

    public void setIdEstado(int idEstado) {
        IdEstado = idEstado;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    @Override
    public String toString(){
        return getNombre();
    }

}
