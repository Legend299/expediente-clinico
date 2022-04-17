package com.expedienteclinico.dto;

import java.io.Serializable;
import java.util.Date;
import java.util.List;

public class ExpedienteDTO implements Serializable {

    private int IdExpediente;
    private String Nombre;
    private String Apellido;
    private String Imagen;
    private String Curp;
    private EstadoDTO Estado;
    private MunicipioDTO Municipio;
    private boolean Sexo;
    private String Telefono;
    private Date FechaDeNacimiento;
    private List<ConsultaDTO> Consulta;
    private List<DocumentoDTO> Documento;

    public ExpedienteDTO() {
    }

    public ExpedienteDTO(int idExpediente, String nombre, String apellido, String imagen, String curp, EstadoDTO estado, MunicipioDTO municipio, boolean sexo, String telefono, Date fechaDeNacimiento, List<ConsultaDTO> consulta, List<DocumentoDTO> documento) {
        IdExpediente = idExpediente;
        Nombre = nombre;
        Apellido = apellido;
        Imagen = imagen;
        Curp = curp;
        Estado = estado;
        Municipio = municipio;
        Sexo = sexo;
        Telefono = telefono;
        FechaDeNacimiento = fechaDeNacimiento;
        Consulta = consulta;
        Documento = documento;
    }

    public int getIdExpediente() {
        return IdExpediente;
    }

    public void setIdExpediente(int idExpediente) {
        IdExpediente = idExpediente;
    }

    public String getNombre() {
        return Nombre;
    }

    public void setNombre(String nombre) {
        Nombre = nombre;
    }

    public String getApellido() {
        return Apellido;
    }

    public void setApellido(String apellido) {
        Apellido = apellido;
    }

    public String getImagen() {
        return Imagen;
    }

    public void setImagen(String imagen) {
        Imagen = imagen;
    }

    public String getCurp() {
        return Curp;
    }

    public void setCurp(String curp) {
        Curp = curp;
    }

    public EstadoDTO getEstado() {
        return Estado;
    }

    public void setEstado(EstadoDTO estado) {
        Estado = estado;
    }

    public MunicipioDTO getMunicipio() {
        return Municipio;
    }

    public void setMunicipio(MunicipioDTO municipio) {
        Municipio = municipio;
    }

    public boolean isSexo() {
        return Sexo;
    }

    public void setSexo(boolean sexo) {
        Sexo = sexo;
    }

    public String getTelefono() {
        return Telefono;
    }

    public void setTelefono(String telefono) {
        Telefono = telefono;
    }

    public Date getFechaDeNacimiento() {
        return FechaDeNacimiento;
    }

    public void setFechaDeNacimiento(Date fechaDeNacimiento) {
        FechaDeNacimiento = fechaDeNacimiento;
    }

    public List<ConsultaDTO> getConsulta() {
        return Consulta;
    }

    public void setConsulta(List<ConsultaDTO> consulta) {
        Consulta = consulta;
    }

    public List<DocumentoDTO> getDocumento() {
        return Documento;
    }

    public void setDocumento(List<DocumentoDTO> documento) {
        Documento = documento;
    }
}
