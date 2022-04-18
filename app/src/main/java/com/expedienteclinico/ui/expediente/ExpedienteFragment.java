package com.expedienteclinico.ui.expediente;

import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.fragment.app.FragmentManager;
import androidx.fragment.app.FragmentTransaction;
import androidx.lifecycle.ViewModelProvider;

import com.android.volley.Request;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.expedienteclinico.Controlador.Expediente.ServicioExpediente;
import com.expedienteclinico.Controlador.SessionUsuario;
import com.expedienteclinico.Controlador.Usuario.ServicioEstado;
import com.expedienteclinico.LoginActivity;
import com.expedienteclinico.R;
import com.expedienteclinico.databinding.FragmentExpedienteBinding;
import com.expedienteclinico.dto.EstadoDTO;
import com.expedienteclinico.dto.ExpedienteDTO;
import com.expedienteclinico.dto.UsuarioDTO;
import com.expedienteclinico.utilidades.ConvertirFecha;
import com.google.android.material.navigation.NavigationView;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import org.w3c.dom.Text;

import java.io.Serializable;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.TimeUnit;

public class ExpedienteFragment extends Fragment {

    private FragmentExpedienteBinding binding;
    private SessionUsuario sessionUsuario = SessionUsuario.getInstance();
    private ExpedienteDTO expedienteUsuario;
    private ProgressBar carga;
    private Button btnEditar;
    private ConvertirFecha _conConvertirFecha;

    private List<EstadoDTO> listaEstados;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        ExpedienteViewModel expedienteViewModel =
                new ViewModelProvider(this).get(ExpedienteViewModel.class);

        binding = FragmentExpedienteBinding.inflate(inflater, container, false);
        View root = binding.getRoot();



        _conConvertirFecha = new ConvertirFecha();
        carga = root.findViewById(R.id.idLoadingPB);
        btnEditar = root.findViewById(R.id.btnEditar);
        //getExpediente();

        carga.setVisibility(View.VISIBLE);

        ServicioExpediente servicioExpediente = new ServicioExpediente(getContext());
        servicioExpediente.solicitarExpedientePorId(String.valueOf(SessionUsuario.getInstance().getIdExpediente()), new ServicioExpediente.VolleyResponseListener() {
            @Override
            public void onError(String message) {
                carga.setVisibility(View.GONE);
                Toast.makeText(getContext(), "NO SE HA PODIDO OBTENER EL EXPEDIENTE", Toast.LENGTH_SHORT).show();
            }

            @Override
            public void onResponse(ExpedienteDTO expediente) {
                carga.setVisibility(View.GONE);
                btnEditar.setEnabled(true);
                //Toast.makeText(getContext(), "EXPEDIENTE ACTUALIZADO", Toast.LENGTH_SHORT).show();

                TextView txtIdExpediente = root.findViewById(R.id.lblShowIdExpediente);
                txtIdExpediente.setText(String.valueOf(expediente.getIdExpediente()));

                TextView txtNombre = root.findViewById(R.id.lblShowNombre);
                txtNombre.setText(expediente.getNombre());

                TextView txtApellido = root.findViewById(R.id.lblShowApellido);
                txtApellido.setText(expediente.getApellido());

                TextView txtCurp = root.findViewById(R.id.lblShowCurp);
                txtCurp.setText(expediente.getCurp());

                //Estado y Municipio
                TextView txtEstado = root.findViewById(R.id.lblShowEstado);
                txtEstado.setText(expediente.getEstado().getNombre());

                TextView txtMunicipio = root.findViewById(R.id.lblShowMunicipio);
                txtMunicipio.setText(expediente.getMunicipio().getNombre());

                TextView txtSexo = root.findViewById(R.id.lblShowSexo);
                if(expediente.isSexo()){
                    txtSexo.setText("Hombre");
                } else {
                    txtSexo.setText("Mujer");
                }

                TextView txtTelefono = root.findViewById(R.id.lblShowTelefono);
                txtTelefono.setText(expediente.getTelefono());

                //Fecha de nacimiento
                TextView txtFecha = root.findViewById(R.id.lblShowFechaDeNacimiento);

                txtFecha.setText(_conConvertirFecha.DateToString(expediente.getFechaDeNacimiento(), "dd/MMMM/yyyy"));

                expedienteUsuario = expediente;

            }
        });

        ServicioEstado servicioEstado = new ServicioEstado(getContext());
        servicioEstado.solicitarListaEstados(new ServicioEstado.VolleyResponseListener() {
            @Override
            public void onError(String message) {

            }

            @Override
            public void onResponse(List<EstadoDTO> estado) {
                listaEstados = new ArrayList<>();
                listaEstados = estado;
            }
        });

        btnEditar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                Bundle bundle=new Bundle();
                bundle.putSerializable("expedienteUsuarioObj",expedienteUsuario);
                bundle.putSerializable("listaEstadosObj", (Serializable) listaEstados);

                Fragment fragment = new ExpedienteEditarFragment();
                fragment.setArguments(bundle);
                FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
                FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                fragmentTransaction.replace(R.id.showExpediente, fragment);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commit();

                btnEditar.setVisibility(View.GONE);

            }
        });

        return root;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

}
