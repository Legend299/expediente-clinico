package com.expedienteclinico.dto;

import java.io.Serializable;

public class MunicipioDTO implements Serializable {

    private int IdMunicipio;
    private String Nombre;

    public MunicipioDTO() {
    }

    public MunicipioDTO(int idMunicipio, String nombre) {
        IdMunicipio = idMunicipio;
        Nombre = nombre;
    }

    public int getIdMunicipio() {
        return IdMunicipio;
    }

    public void setIdMunicipio(int idMunicipio) {
        IdMunicipio = idMunicipio;
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
