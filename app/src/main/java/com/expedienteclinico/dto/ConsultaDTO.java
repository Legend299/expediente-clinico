package com.expedienteclinico.dto;

import java.util.Date;

public class ConsultaDTO {

    private int IdConsulta;
    private Date Fecha;
    private String Medico;
    private int IdTipoConsulta;
    private int IdExpediente;
    private String Diagnostico;

    public ConsultaDTO() {
    }

    public ConsultaDTO(int idConsulta, Date fecha, String medico, int idTipoConsulta, int idExpediente, String diagnostico) {
        IdConsulta = idConsulta;
        Fecha = fecha;
        Medico = medico;
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

    public String getMedico() {
        return Medico;
    }

    public void setMedico(String medico) {
        Medico = medico;
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
