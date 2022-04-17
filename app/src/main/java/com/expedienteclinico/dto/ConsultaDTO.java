package com.expedienteclinico.dto;

import java.util.Date;

public class ConsultaDTO {

    private int IdConsulta;
    private Date Fecha;
    private int IdUsuario;
    private int IdTipoConsulta;
    private int IdExpediente;
    private String Diagnostico;

    public ConsultaDTO() {
    }

    public ConsultaDTO(int idConsulta, Date fecha, int idUsuario, int idTipoConsulta, int idExpediente, String diagnostico) {
        IdConsulta = idConsulta;
        Fecha = fecha;
        IdUsuario = idUsuario;
        IdTipoConsulta = idTipoConsulta;
        IdExpediente = idExpediente;
        Diagnostico = diagnostico;
    }

    public int getIdConsulta() {
        return IdConsulta;
    }

    public void setIdConsulta(int idConsulta) {
        IdConsulta = idConsulta;
    }

    public Date getFecha() {
        return Fecha;
    }

    public void setFecha(Date fecha) {
        Fecha = fecha;
    }

    public int getIdUsuario() {
        return IdUsuario;
    }

    public void setIdUsuario(int idUsuario) {
        IdUsuario = idUsuario;
    }

    public int getIdTipoConsulta() {
        return IdTipoConsulta;
    }

    public void setIdTipoConsulta(int idTipoConsulta) {
        IdTipoConsulta = idTipoConsulta;
    }

    public int getIdExpediente() {
        return IdExpediente;
    }

    public void setIdExpediente(int idExpediente) {
        IdExpediente = idExpediente;
    }

    public String getDiagnostico() {
        return Diagnostico;
    }

    public void setDiagnostico(String diagnostico) {
        Diagnostico = diagnostico;
    }
}
