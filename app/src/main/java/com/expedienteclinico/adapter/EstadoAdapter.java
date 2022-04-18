package com.expedienteclinico.adapter;

import android.content.Context;
import android.graphics.Color;
import android.media.metrics.Event;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.expedienteclinico.dto.EstadoDTO;

public class EstadoAdapter extends ArrayAdapter<EstadoDTO> {

    private Context _context;
    private EstadoDTO[] _values;

    public EstadoAdapter(Context context, int textViewResourceId, EstadoDTO[] values){
        super(context, textViewResourceId, values);
        this._context = context;
        this._values = values;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent){
        TextView label = (TextView) super.getView(position, convertView, parent);
        label.setTextColor(Color.BLACK);
        label.setText(_values[position].getNombre());
        return label;
    }

    @Override
    public View getDropDownView(int position, View convertView, ViewGroup parent){
        TextView label = (TextView) super.getDropDownView(position, convertView, parent);
        label.setTextColor(Color.BLACK);
        label.setText(_values[position].getNombre());
        return label;
    }


}
