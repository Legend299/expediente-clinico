package com.expedienteclinico.ui.consulta;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;

import com.expedienteclinico.Controlador.Consulta.ServicioConsulta;
import com.expedienteclinico.Controlador.SessionUsuario;
import com.expedienteclinico.R;
import com.expedienteclinico.databinding.FragmentConsultaBinding;
import com.expedienteclinico.dto.ConsultaDTO;

import java.util.ArrayList;
import java.util.List;

public class ConsultaFragment extends Fragment {

    private FragmentConsultaBinding binding;
    private ProgressBar carga;
    private ListView listaConsulta;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        ConsultaViewModel consultaViewModel =
                new ViewModelProvider(this).get(ConsultaViewModel.class);

        binding = FragmentConsultaBinding.inflate(inflater, container, false);
        View root = binding.getRoot();

        carga = root.findViewById(R.id.idLoadingPB);
        listaConsulta = root.findViewById(R.id.listViewConsulta);

        carga.setVisibility(View.VISIBLE);
        ServicioConsulta servicioConsulta = new ServicioConsulta(getContext());
        servicioConsulta.SolicitarConsultasUsuario(SessionUsuario.getInstance().getIdExpediente(), new ServicioConsulta.VolleyResponseListener() {
            @Override
            public void onError(String message) {
                carga.setVisibility(View.GONE);
                Toast.makeText(getContext(),"No se ha podido recibir las consultas", Toast.LENGTH_LONG).show();
            }

            @Override
            public void onResponse(List<ConsultaDTO> response) {
                carga.setVisibility(View.GONE);
                //Toast.makeText(getContext(),response, Toast.LENGTH_LONG).show();
                ConsultaListaAdapter adapter = new ConsultaListaAdapter(response, getContext());
                //adding the adapter to listview
                listaConsulta.setAdapter(adapter);
            }
        });

        //final ListView textView = binding.listViewConsulta;
        //consultaViewModel.getText().observe(getViewLifecycleOwner(), textView::setText);
        return root;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }
}