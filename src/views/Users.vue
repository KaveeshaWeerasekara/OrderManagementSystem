<template>
  <v-container>
    <v-row justify="space-between" align="center">
      <v-col>
        <v-toolbar flat>
          <v-toolbar-title>Users List</v-toolbar-title>
        </v-toolbar>
      </v-col>
      <v-col class="text-right">
        <v-btn color="primary" @click="navigateToCreateUser">Create User</v-btn>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-data-table
          :headers="headers"
          :items="users"
          class="elevation-1"
          item-value="id"
          fixed-header
        >
        <template v-slot:[`item.actions`]="{ item }">
          <v-icon
      small
      @click="editUser(item)"
      class="mr-2"
      color="blue"
    >
      mdi-pencil
    </v-icon>
    <v-icon
      small
      @click="viewDetails(item)"
      class="mr-2"
      color="green"
    >
      mdi-information
    </v-icon>
    <v-icon
      small
      @click="deleteUser(item.id)"
      color="red"
    >
      mdi-delete
    </v-icon>
  </template>
        </v-data-table>
      </v-col>
    </v-row>
    <v-dialog v-model="dialog" max-width="500px">
      <v-card>
        <v-card-title>User Details</v-card-title>
        <v-card-text>
          <p><strong>First Name:</strong> {{ selectedUser?.fName }}</p>
          <p><strong>Last Name:</strong> {{ selectedUser?.lName }}</p>
          <p><strong>Email:</strong> {{ selectedUser?.email }}</p>
          <p><strong>Address:</strong> {{ selectedUser?.address }}</p>
          <p><strong>Phone Number:</strong> {{ selectedUser?.phoneNumber }}</p>
        </v-card-text>
        <v-card-actions>
          <v-btn color="primary" @click="dialog = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script>
import axios from 'axios';
import { useRouter } from 'vue-router';
import UserForm from '../components/UserForm.vue'

export default {
  data() {
    return {
      headers: [
        { title: 'First Name', value: 'fName' },
        { title: 'Last Name', value: 'lName' },
        { title: 'Email', value: 'email' },
        { title: 'Address', value: 'address' },
        { title: 'Phone Number', value: 'phoneNumber' },
        { title: 'Action', value: 'actions', sortable: false },
      ],
      users: [],
      dialog: false,
      selectedUser: null,
    };
  },
  created() {
    this.fetchUsers();
  },
  methods: {
    editUser(user) {
    this.$router.push({ path: `/edit-user/${user.id}` });
  },
viewDetails(user) {
    this.selectedUser = user;
    this.dialog = true;
  },
   async deleteUser(id) {
    if (confirm('Are you sure you want to delete this user?')) {
      try {
        await axios.delete(`http://localhost:5099/api/User/DeleteUserById/${id}`);
        this.fetchUsers(); // Refresh the user list after deletion
      } catch (error) {
        console.error('Error deleting user:', error);
      }
    }
  },
    async fetchUsers() {
      try {
        const response = await axios.get('http://localhost:5099/api/User/GetList');
        if (response.data.status_code === 200) {
          this.users = response.data.data.users;
        } else {
          console.error('Failed to load users');
        }
      } catch (error) {
        console.error('Error fetching users:', error);
      }
    },
    navigateToCreateUser() {
      this.$router.push('/create-user');
    },
  },
};
</script>

<style scoped>
.elevation-1 {
  margin-top: 20px;
}
.v-toolbar-title {
  font-weight: bold;
}
</style>







