package com.expedienteclinico.dto;

public class DocumentoDTO {

    private int IdDocumento;
    private String Nombre;
    private String Extension;
    private String Ruta;
    private String Medico;
    private int Peso;
    private int IdExpediente;

    public DocumentoDTO() {
    }

    public DocumentoDTO(int idDocumento, String nombre, String extension, String ruta, String medico, int peso, int idExpediente) {
        IdDocumento = idDocumento;
        Nombre = nombre;
        Extension = extension;
        Ruta = ruta;
        Medico = medico;
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

    public String getMedico() {
        return Medico;
    }

    public void setMedico(String medico) {
        Medico = medico;
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
