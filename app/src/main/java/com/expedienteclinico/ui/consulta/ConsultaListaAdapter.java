package com.expedienteclinico.ui.consulta;

import android.content.Context;
import android.widget.ArrayAdapter;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.expedienteclinico.R;
import com.expedienteclinico.dto.ConsultaDTO;
import com.expedienteclinico.utilidades.ConvertirFecha;

import java.util.List;

public class ConsultaListaAdapter extends ArrayAdapter<ConsultaDTO> {

    private List<ConsultaDTO> _listaConsultas;
    private Context _context;
    private ConvertirFecha _convertirFecha;

    public ConsultaListaAdapter(List<ConsultaDTO> listaConsultas, Context context) {
        super(context, R.layout.adapter_view_consulta_layout, listaConsultas);
        _listaConsultas = listaConsultas;
        _context = context;
        _convertirFecha = new ConvertirFecha();
    }
    //this method will return the list item
    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        LayoutInflater inflater = LayoutInflater.from(_context);

        View listViewItem = inflater.inflate(R.layout.adapter_view_consulta_layout, null, true);

        TextView txtFechaConsulta = listViewItem.findViewById(R.id.txtFechaConsulta);
        TextView txtDiagnostico = listViewItem.findViewById(R.id.txtDiagnostico);
        TextView txtMedico = listViewItem.findViewById(R.id.txtMedico);

        ConsultaDTO consulta = _listaConsultas.get(position);

        txtFechaConsulta.setText(_convertirFecha.DateToString(consulta.getFecha(),"dd/MMMM/yyyy"));
        txtDiagnostico.setText(consulta.getDiagnostico());
        txtMedico.setText(consulta.getMedico());

        return listViewItem;
    }

}
