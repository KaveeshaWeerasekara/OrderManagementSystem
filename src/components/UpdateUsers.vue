<template>
  <v-container>
    <v-form ref="form" v-model="valid" @submit.prevent="submitForm">
      <v-row>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="user.fName"
            :rules="[rules.required]"
            label="First Name"
            required
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="user.lName"
            :rules="[rules.required]"
            label="Last Name"
            required
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="user.email"
            :rules="[rules.required, rules.email]"
            label="Email"
            required
          ></v-text-field>
        </v-col>
        <v-col cols="12" md="6">
          <v-text-field
            v-model="user.phoneNumber"
            :rules="[rules.required, rules.phone]"
            label="Phone Number"
            required
          ></v-text-field>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12">
          <v-textarea
            v-model="user.address"
            :rules="[rules.required]"
            label="Address"
            required
          ></v-textarea>
        </v-col>
      </v-row>
      <v-btn color="primary" type="submit">Update</v-btn>&nbsp;
      <v-btn color="secondary" @click="clearForm">Clear</v-btn>
    </v-form>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import axios from 'axios';
import { useRoute, useRouter } from 'vue-router';

const route = useRoute();
const router = useRouter();

const valid = ref(false);
const user = ref({
  fName: '',
  lName: '',
  email: '',
  address: '',
  phoneNumber: '',
});

const rules = {
  required: value => !!value || 'Required.',
  email: value => /.+@.+\..+/.test(value) || 'E-mail must be valid.',
  phone: value => /^\d+$/.test(value) || 'Phone number must be valid.',
};

onMounted(async () => {
  try {
    const userId = route.params.id;
    console.log(userId);
    const response = await axios.get(`http://localhost:5099/api/User/GetUserById/${userId}`);
    console.log(response);
    if (response.status === 200) {
      user.value = response.data;
    } else {
      console.error('Failed to load user data');
    }
  } catch (error) {
    console.error('Error fetching user data:', error);
  }
});

const submitForm = async () => {
  if (valid.value) {
    try {
      const userId = route.params.id;
      const response = await axios.put(`http://localhost:5099/api/User/UpdateUserById/${userId}`, user.value);

      if (response.status === 200) {
        console.log('User updated successfully');
        router.push('/users');
        clearForm();
      alert("User Update Successfully");
      } else {
        console.error('Failed to update user');
      }
    } catch (error) {
      console.error('Error updating user:', error);
    }
  }
};
const clearForm = () => {
  user.value = {
    fName: '',
    lName: '',
    email: '',
    address: '',
    phoneNumber: '',
  };
};
</script>

<style scoped>
/* Add any scoped styles here if needed */
</style>
