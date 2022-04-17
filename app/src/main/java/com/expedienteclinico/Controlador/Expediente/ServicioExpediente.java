package com.expedienteclinico.Controlador.Expediente;

import android.content.Context;
import android.util.Log;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.expedienteclinico.API;
import com.expedienteclinico.Controlador.SessionUsuario;
import com.expedienteclinico.LoginActivity;
import com.expedienteclinico.dto.ConsultaDTO;
import com.expedienteclinico.dto.DocumentoDTO;
import com.expedienteclinico.dto.EstadoDTO;
import com.expedienteclinico.dto.ExpedienteDTO;
import com.expedienteclinico.dto.MunicipioDTO;
import com.expedienteclinico.utilidades.ConvertirFecha;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ServicioExpediente {

    Context _context;
    ExpedienteDTO _expediente;
    ConvertirFecha _convertirFecha;

    public ServicioExpediente(Context context) {
        _context = context;
        _expediente = new ExpedienteDTO();
        _convertirFecha = new ConvertirFecha();
    }

    public interface VolleyResponseListener{
        void onError(String message);
        void onResponse(ExpedienteDTO expediente);
    }

    public interface VolleyResponseListenerEditar{
        void onError(String message);
        void onResponse(JSONObject obj);
    }

    public void solicitarExpedientePorId(String IdExpediente, VolleyResponseListener volleyResponseListener) {
        String url = API.QUERY_EXPEDIENTE_ID + IdExpediente;
        StringRequest postRequest = new StringRequest(Request.Method.GET, url, new Response.Listener<String>() {
            @Override
            public void onResponse(String response) {
                try {
                        JSONObject jsonObject = new JSONObject(response);

                        _expediente.setIdExpediente(jsonObject.getInt("idExpediente"));
                        _expediente.setNombre(jsonObject.getString("nombre"));
                        _expediente.setApellido(jsonObject.getString("apellido"));
                        _expediente.setCurp(jsonObject.getString("curp"));

                        //Estado
                        JSONObject jsonEstado = new JSONObject(jsonObject.getString("estado"));
                        _expediente.setEstado(new EstadoDTO(
                                jsonEstado.getInt("idEstado"),
                                jsonEstado.getString("nombre")
                        ));

                        JSONObject jsonMunicipio = new JSONObject(jsonObject.getString("municipio"));
                        _expediente.setMunicipio(new MunicipioDTO(
                                jsonMunicipio.getInt("idMunicipio"),
                                jsonMunicipio.getString("nombre")
                        ));


                        _expediente.setSexo(jsonObject.getBoolean("sexo"));
                        _expediente.setTelefono(jsonObject.getString("telefono"));

                        _expediente.setFechaDeNacimiento(_convertirFecha.recibirFecha(jsonObject.getString("fechaDeNacimiento")));

                        //LISTA CONSULTA
                        List<ConsultaDTO> lstConsulta = new ArrayList<>();

                        JSONArray jsonConsulta = jsonObject.getJSONArray("consulta");
                        for (int i = 0; i < jsonConsulta.length(); i++){
                            ConsultaDTO consulta = new ConsultaDTO();
                            JSONObject jsonObjectConsulta = jsonConsulta.getJSONObject(i);

                            consulta.setIdConsulta(jsonObjectConsulta.getInt("idConsulta"));
                            consulta.setFecha(_convertirFecha.recibirFecha(jsonObjectConsulta.getString("fecha")));
                            consulta.setIdUsuario(jsonObjectConsulta.getInt("idUsuario"));
                            consulta.setIdTipoConsulta(jsonObjectConsulta.getInt("idTipoConsulta"));
                            consulta.setIdExpediente(jsonObjectConsulta.getInt("idExpediente"));
                            consulta.setDiagnostico(jsonObjectConsulta.getString("diagnostico"));

                            lstConsulta.add(consulta);
                        }

                        _expediente.setConsulta(lstConsulta);

                        //LISTA DOCUMENTOS
                        List<DocumentoDTO> lstDocumento = new ArrayList<>();

                        JSONArray jsonDocumento = jsonObject.getJSONArray("documento");
                        for (int i = 0; i < jsonDocumento.length(); i++){
                            DocumentoDTO documento = new DocumentoDTO();
                            JSONObject jsonObjectDocumento = jsonDocumento.getJSONObject(i);

                            documento.setIdDocumento(jsonObjectDocumento.getInt("idDocumento"));
                            documento.setNombre(jsonObjectDocumento.getString("nombre"));
                            documento.setExtension(jsonObjectDocumento.getString("extension"));
                            documento.setRuta(jsonObjectDocumento.getString("ruta"));
                            documento.setIdUsuario(jsonObjectDocumento.getInt("idUsuario"));
                            documento.setPeso(jsonObjectDocumento.getInt("peso"));
                            documento.setIdExpediente(jsonObjectDocumento.getInt("idExpediente"));


                            lstDocumento.add(documento);
                        }

                        _expediente.setDocumento(lstDocumento);

                        volleyResponseListener.onResponse(_expediente);

                } catch (JSONException e) {
                    e.printStackTrace();
                }
            }
        }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                volleyResponseListener.onError(error.getMessage());
            }
        });
        Volley.newRequestQueue(_context).add(postRequest);
    }

    public void editarExpediente(ExpedienteDTO _expediente, VolleyResponseListenerEditar volleyResponseListener){
        String url = API.QUERY_EXPEDIENTE_ID.toString();

        JSONObject postData = new JSONObject();
        try{

            postData.put("idExpediente", _expediente.getIdExpediente());
            postData.put("nombre", _expediente.getNombre());
            postData.put("apellido",_expediente.getApellido());
            postData.put("imagen", _expediente.getImagen());
            postData.put("curp",_expediente.getCurp());
            postData.put("idEstado",_expediente.getEstado().getIdEstado());
            postData.put("idMunicipio",_expediente.getMunicipio().getIdMunicipio());
            postData.put("sexo",_expediente.isSexo());
            postData.put("telefono",_expediente.getTelefono());

            postData.put("fechaDeNacimiento", _convertirFecha.DateToString(_expediente.getFechaDeNacimiento(),"yyyy/MMMM/dd"));

        }catch(JSONException e){
            e.printStackTrace();
        }

        JsonObjectRequest putRequest = new JsonObjectRequest(Request.Method.PUT, url, postData, new Response.Listener<JSONObject>() {
                @Override
                public void onResponse(JSONObject response) {
                    volleyResponseListener.onResponse(response);
                }
            }, new Response.ErrorListener() {
                @Override
                public void onErrorResponse(VolleyError error) {
                    volleyResponseListener.onError(error.getMessage());
                }
            });
            Volley.newRequestQueue(_context).add(putRequest);

    }
}
