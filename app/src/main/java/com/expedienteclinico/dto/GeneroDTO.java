package com.expedienteclinico.dto;

public class GeneroDTO {
    private boolean Genero;
    private String Nombre;

    public GeneroDTO() {
    }

    public GeneroDTO(boolean genero, String nombre) {
        Genero = genero;
        Nombre = nombre;
    }

    public boolean getGenero() {
        return Genero;
    }

    public void setGenero(boolean genero) {
        Genero = genero;
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
