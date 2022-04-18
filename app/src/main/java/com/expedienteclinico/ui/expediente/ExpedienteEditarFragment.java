package com.expedienteclinico.ui.expediente;

import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.os.Build;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.annotation.RequiresApi;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.lifecycle.ViewModelProvider;

import com.expedienteclinico.Controlador.Expediente.ServicioExpediente;
import com.expedienteclinico.Controlador.Usuario.ServicioEstado;
import com.expedienteclinico.R;
import com.expedienteclinico.adapter.EstadoAdapter;
import com.expedienteclinico.databinding.FragmentExpedienteEditarBinding;
import com.expedienteclinico.dto.EstadoDTO;
import com.expedienteclinico.dto.ExpedienteDTO;
import com.expedienteclinico.dto.GeneroDTO;
import com.expedienteclinico.utilidades.ConvertirFecha;

import org.json.JSONObject;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;

public class ExpedienteEditarFragment extends Fragment /*implements AdapterView.OnItemSelectedListener*/{

    private FragmentExpedienteEditarBinding binding;
    private Button btnCancelar,btnEditar,dateButton;
    private DatePickerDialog datePickerDialog;
    private EditText txtNombre, txtApellido, txtCurp, txtTelefono;

    private TextView txtIdExpediente;

    private ProgressBar carga;
    private ConvertirFecha _convertirFecha;

    //SPINNER
    private Spinner spGenero,spEstado;

    //Lista
    private List<EstadoDTO> listaEstados;

    @RequiresApi(api = Build.VERSION_CODES.N)
    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        ExpedienteViewModel expedienteViewModel =
                new ViewModelProvider(this).get(ExpedienteViewModel.class);

        binding = FragmentExpedienteEditarBinding.inflate(inflater, container, false);
        View root = binding.getRoot();
        _convertirFecha = new ConvertirFecha();

        carga = root.findViewById(R.id.idLoadingPB);
        btnCancelar = root.findViewById(R.id.btnCancelar);
        btnEditar = root.findViewById(R.id.btnEditar);

        //editText
        txtIdExpediente = root.findViewById(R.id.txtShowIdExpediente);
        txtNombre = root.findViewById(R.id.txtShowNombre);
        txtApellido = root.findViewById(R.id.txtShowApellido);
        txtCurp = root.findViewById(R.id.txtShowCurp);

        txtTelefono = root.findViewById(R.id.txtShowTelefono);

        //

        ExpedienteDTO data = (ExpedienteDTO)getArguments().getSerializable("expedienteUsuarioObj");
        listaEstados = (List<EstadoDTO>)getArguments().getSerializable("listaEstadosObj");
        //Toast.makeText(getContext(),"DATA: "+data.getNombre(), Toast.LENGTH_LONG).show();


        //Asignar GET
        txtIdExpediente.setText(String.valueOf(data.getIdExpediente()));
        txtNombre.setText(data.getNombre());
        txtApellido.setText(data.getApellido());
        txtCurp.setText(data.getCurp());


        //ESTADO INICIO
        ArrayAdapter estadoAdapter = new ArrayAdapter(getContext(), R.layout.spinner_layout, listaEstados);
        spEstado = (Spinner) root.findViewById(R.id.spShowEstado);
        spEstado.setAdapter(estadoAdapter);

        spEstado.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                EstadoDTO estado = (EstadoDTO) adapterView.getSelectedItem();
                Toast.makeText(getContext(),"NOMBRE: "+estado.getNombre()+"\n"+"ID: "+estado.getIdEstado(), Toast.LENGTH_LONG).show();
                data.setEstado(estado);
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });

        //ESTADO FIN

        //GENERO INICIO
        ArrayAdapter generoAdapter = new ArrayAdapter(getContext(), R.layout.spinner_layout, listarGeneros());
        spGenero = (Spinner) root.findViewById(R.id.spShowSexo);
        spGenero.setAdapter(generoAdapter);
        if(data.isSexo()){
            spGenero.setSelection(0);
        } else {
            spGenero.setSelection(1);
        }
        spGenero.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> adapterView, View view, int i, long l) {
                GeneroDTO genero = (GeneroDTO) adapterView.getSelectedItem();
                //Toast.makeText(getContext(),"NOMBRE: "+genero.getNombre()+"\n"+"BOOLEAN: "+genero.getGenero(), Toast.LENGTH_LONG).show();
                data.setSexo(genero.getGenero());
            }

            @Override
            public void onNothingSelected(AdapterView<?> adapterView) {

            }
        });
        //GENERO FIN

        txtTelefono.setText(data.getTelefono());
        //

        dateButton = root.findViewById(R.id.lblShowFechaDeNacimiento);

        dateButton.setText(_convertirFecha.DateToString(data.getFechaDeNacimiento(), "dd/MMMM/yyyy"));

        dateButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //initDatePicker(data.getFechaDeNacimiento().getDay(),
                //        data.getFechaDeNacimiento().getMonth(),
                //        1998);
                //initDatePicker();
                DatePickerDialog.OnDateSetListener dateSetListener = new DatePickerDialog.OnDateSetListener() {
                    @Override
                    public void onDateSet(DatePicker datePicker, int year, int month, int day) {
                        month = month + 1;
                        String date = makeDateString(day, month, year);
                        dateButton.setText(date);
                    }
                };

                int style = AlertDialog.THEME_HOLO_LIGHT;

                SimpleDateFormat dayFormat = new SimpleDateFormat("dd");
                String day = dayFormat.format(data.getFechaDeNacimiento());

                SimpleDateFormat monthFormat = new SimpleDateFormat("MMMM");
                String month = monthFormat.format(data.getFechaDeNacimiento());

                SimpleDateFormat yearFormat = new SimpleDateFormat("yyyy");
                String year = yearFormat.format(data.getFechaDeNacimiento());

                //Toast.makeText(getContext(),"FECHA ?: "+day+"/"+month+"/"+year, Toast.LENGTH_LONG).show();

                int dayN = Integer.valueOf(day);
                int monthN = data.getFechaDeNacimiento().getMonth();
                int yearN = Integer.valueOf(year);

                datePickerDialog  = new DatePickerDialog(getContext(), style, dateSetListener, yearN,monthN,dayN );

                datePickerDialog.show();
            }
        });

        btnCancelar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Fragment fragment = new ExpedienteFragment();
                FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                fragmentTransaction.replace(R.id.ShowExpedienteEditar, fragment);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();

                btnCancelar.setVisibility(View.GONE);
                btnEditar.setVisibility(View.GONE);
                dateButton.setVisibility(View.GONE);

            }
        });

        btnEditar.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View view) {

                //Carga
                carga.setVisibility(View.VISIBLE);
                btnEditar.setVisibility(View.GONE);
                btnCancelar.setVisibility(View.GONE);
                dateButton.setVisibility(View.GONE);

                data.setNombre(txtNombre.getText().toString());
                data.setApellido(txtApellido.getText().toString());
                data.setCurp(txtCurp.getText().toString());
                data.setTelefono(txtTelefono.getText().toString());

                data.setFechaDeNacimiento(_convertirFecha.recibirFechaDatePicker(dateButton.getText().toString()));

                ServicioExpediente servicioExpediente = new ServicioExpediente(getContext());
                servicioExpediente.editarExpediente(data, new ServicioExpediente.VolleyResponseListenerEditar() {
                    @Override
                    public void onError(String message) {
                        carga.setVisibility(View.GONE);

                        btnEditar.setVisibility(View.VISIBLE);
                        btnCancelar.setVisibility(View.VISIBLE);
                        dateButton.setVisibility(View.VISIBLE);

                        Toast.makeText(getContext(),"NO SE HA PODIDO MODIFICAR EL EXPEDIENTE", Toast.LENGTH_LONG).show();
                    }

                    @Override
                    public void onResponse(JSONObject obj) {
                        //carga.setVisibility(View.GONE);
                        Fragment fragment = new ExpedienteFragment();
                        FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
                        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                        fragmentTransaction.replace(R.id.ShowExpedienteEditar, fragment);
                        fragmentTransaction.addToBackStack(null);
                        fragmentTransaction.commit();

                        btnCancelar.setVisibility(View.GONE);
                        btnEditar.setVisibility(View.GONE);
                        dateButton.setVisibility(View.GONE);
                    }
                });
                //Toast.makeText(getContext(),"FECHA ?: "+dateButton.getText().toString(), Toast.LENGTH_LONG).show();
            }
        });

        return root;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

    private String makeDateString(int day, int month, int year){
        return day + "/" + getMonthFormat(month) + "/" + year;
    }

    private String getMonthFormat(int month){
        switch (month){
            case 1:
                return "enero";
            case 2:
                return "febrero";
            case 3:
                return "marzo";
            case 4:
                return "abril";
            case 5:
                return "mayo";
            case 6:
                return "junio";
            case 7:
                return "julio";
            case 8:
                return "agosto";
            case 9:
                return "septiembre";
            case 10:
                return "octubre";
            case 11:
                return "noviembre";
            case 12:
                return "diciembre";

        }

        return "enero";

    }

    private List<GeneroDTO> listarGeneros(){
        GeneroDTO genero1 = new GeneroDTO();
        genero1.setGenero(true);
        genero1.setNombre("Hombre");

        GeneroDTO genero2 = new GeneroDTO();
        genero2.setGenero(false);
        genero2.setNombre("Mujer");

        List<GeneroDTO> listaGeneros = new ArrayList<>();
        listaGeneros.add(genero1);
        listaGeneros.add(genero2);

        return listaGeneros;
    }

}
