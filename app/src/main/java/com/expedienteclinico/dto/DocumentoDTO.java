package com.expedienteclinico.dto;

public class DocumentoDTO {

    private int IdDocumento;
    private String Nombre;
    private String Extension;
    private String Ruta;
    private int IdUsuario;
    private int Peso;
    private int IdExpediente;

    public DocumentoDTO() {
    }

    public DocumentoDTO(int idDocumento, String nombre, String extension, String ruta, int idUsuario, int peso, int idExpediente) {
        IdDocumento = idDocumento;
        Nombre = nombre;
        Extension = extension;
        Ruta = ruta;
        IdUsuario = idUsuario;
        Peso = peso;
        IdExpediente = idExpediente;
    }

    public int getIdDocumento() {
        return IdDocumento;
    }

    public void setIdDocumento(int idDocumento) {
        IdDocumento = idDocumento;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    public String getExtension() {
        return Extension;
    }

    public void setExtension(String extension) {
        Extension = extension;
    }

    public String getRuta() {
        return Ruta;
    }

    public void setRuta(String ruta) {
        Ruta = ruta;
    }

    public int getIdUsuario() {
        return IdUsuario;
    }

    public void setIdUsuario(int idUsuario) {
        IdUsuario = idUsuario;
    }

    public int getPeso() {
        return Peso;
    }

    public void setPeso(int peso) {
        Peso = peso;
    }

    public int getIdExpediente() {
        return IdExpediente;
    }

    public void setIdExpediente(int idExpediente) {
        IdExpediente = idExpediente;
    }
}
